using Indium.Common.EFContexts;
using Indium.Common.EFModels;
using SDSFinder.EFContexts;
using SDSFinder.EFModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SDSFinder.Modules.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDSFinder.Tests.Repositories
{
    [TestClass]
    public class ItemRepositoryTests
    {
        private Mock<IDbContextFactory<IndAppContext>> StubAppContextFactory { get; set; } = null!;

        [TestInitialize]
        public void Init()
        {
            // IND_APPContext
            StubAppContextFactory = new Mock<IDbContextFactory<IndAppContext>>();
            StubAppContextFactory.Setup(f => f.CreateDbContext())
            .Returns(() => new IndAppContext(new DbContextOptionsBuilder<IndAppContext>().UseInMemoryDatabase("InMemoryTest", b => b.EnableNullChecks(false)).Options));
            StubAppContextFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(() => new IndAppContext(new DbContextOptionsBuilder<IndAppContext>().UseInMemoryDatabase("InMemoryTest", b => b.EnableNullChecks(false)).Options));
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await using var appContext = StubAppContextFactory.Object.CreateDbContext();

            appContext.ItemGlbls.RemoveRange(appContext.ItemGlbls);

            await appContext.SaveChangesAsync();
        }

        public async Task<IndAppContext> AddItemsToMockDb(IndAppContext appContext)
        {
            appContext.ItemGlbls.AddRange(
                new ItemGlbl { Item = "010000", Description = "Desc A" },
                new ItemGlbl { Item = "020000", Description = "Desc B" },
                new ItemGlbl { Item = "030000", Description = "Desc C" }
            );
            await appContext.SaveChangesAsync();
            return appContext;
        }

        [TestMethod]
        public async Task ValidateItemSuccess()
        {
            IndAppContext AppContext = StubAppContextFactory.Object.CreateDbContext();

            AppContext = await AddItemsToMockDb(AppContext);
            ItemRepository repo = new();

            var item = "010000";

            var validationResult = await repo.ValidateItem(item, AppContext);
            Assert.IsTrue(validationResult);
        }

        [TestMethod]
        public async Task ValidateItemFailure()
        {
            IndAppContext AppContext = StubAppContextFactory.Object.CreateDbContext();

            AppContext = await AddItemsToMockDb(AppContext);
            ItemRepository repo = new();

            var item = "n/a";

            var validationResult = await repo.ValidateItem(item, AppContext);
            Assert.IsFalse(validationResult);
        }

        [TestMethod]
        public async Task Get_ReturnsCorrectItem()
        {
            var context = await AddItemsToMockDb(StubAppContextFactory.Object.CreateDbContext());
            var repo = new ItemRepository();

            var result = await repo.Get("010000", context);

            Assert.IsNotNull(result);
            Assert.AreEqual("010000", result!.Item);
        }

        [TestMethod]
        public async Task GetBy_ReturnsCorrectItem()
        {
            var context = await AddItemsToMockDb(StubAppContextFactory.Object.CreateDbContext());
            var repo = new ItemRepository();

            var result = await repo.GetBy(x => x.Description.Contains("Desc B"), context);

            Assert.IsNotNull(result);
            Assert.AreEqual("020000", result!.Item);
        }

        [TestMethod]
        public async Task GetLimitedListBy_ReturnsLimitedItems()
        {
            var context = await AddItemsToMockDb(StubAppContextFactory.Object.CreateDbContext());
            var repo = new ItemRepository();

            var results = await repo.GetLimitedListBy(x => x.Item.StartsWith("0"), 2, context);

            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.All(r => r.Item.StartsWith("0")));
        }

        [TestMethod]
        public async Task GetListBy_ReturnsAllMatchingItems()
        {
            var context = await AddItemsToMockDb(StubAppContextFactory.Object.CreateDbContext());
            var repo = new ItemRepository();

            var results = await repo.GetListBy(x => x.Item.StartsWith("0"), context);

            Assert.AreEqual(3, results.Count);
            Assert.IsTrue(results.All(r => r.Item.StartsWith("0")));
        }

    }
}
