using Indium.Common.EFContexts;
using Indium.Common.EFModels;
using Indium.Infor.EFContexts;
using Indium.Infor.EFModels;
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
        private Mock<IDbContextFactory<IND_APPContext>> StubAppContextFactory { get; set; } = null!;

        [TestInitialize]
        public void Init()
        {
            // IND_APPContext
            StubAppContextFactory = new Mock<IDbContextFactory<IND_APPContext>>();
            StubAppContextFactory.Setup(f => f.CreateDbContext())
            .Returns(() => new IND_APPContext(new DbContextOptionsBuilder<IND_APPContext>().UseInMemoryDatabase("InMemoryTest", b => b.EnableNullChecks(false)).Options));
            StubAppContextFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
             .ReturnsAsync(() => new IND_APPContext(new DbContextOptionsBuilder<IND_APPContext>().UseInMemoryDatabase("InMemoryTest", b => b.EnableNullChecks(false)).Options));
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await using var appContext = StubAppContextFactory.Object.CreateDbContext();

            appContext.ItemGlbls.RemoveRange(appContext.ItemGlbls);

            await appContext.SaveChangesAsync();
        }

        public async Task<IND_APPContext> AddItemsToMockDb(IND_APPContext appContext)
        {
            ItemGlbl item = new()
            {
                Item = "J-1234567890",
                Description = "BPD"
            };

            appContext.ItemGlbls.Add(item);

            await appContext.SaveChangesAsync();

            return appContext;
        }

    }
}
