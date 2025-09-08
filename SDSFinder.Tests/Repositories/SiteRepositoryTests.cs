using Indium.Common.EFContexts;
using Indium.Common.EFModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public class SiteRepositoryTests
    {
        private IDbContextFactory<CommonContext> GetInMemoryDbContextFactory(string dbName, bool seedData = true)
        {
            var options = new DbContextOptionsBuilder<CommonContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var factory = new PooledDbContextFactory<CommonContext>(options);

            if (seedData)
            {
                using var context = factory.CreateDbContext();
                context.CmSiteMasters.AddRange(
                    new CmSiteMaster { Id = 1, SiteCode = "A001", SiteName = "Site A", IsActive = true, TimeZone = "UTC" },
                    new CmSiteMaster { Id = 2, SiteCode = "B002", SiteName = "Site B", IsActive = true, TimeZone = "UTC" }
                );
                context.SaveChanges();
            }

            return factory;
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnSitesOrderedBySiteCode()
        {
            // Arrange
            var context = GetInMemoryDbContextFactory("SiteDb_GetAll");
            var repo = new SiteRepository(context);

            // Act
            List<CmSiteMaster> sites = await repo.GetAll();

            // Assert
            Assert.AreEqual(2, sites.Count);
            Assert.AreEqual("A001", sites[0].SiteCode); // Check ordering
            Assert.AreEqual("B002", sites[1].SiteCode);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoSitesExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CommonContext>()
                .UseInMemoryDatabase(databaseName: "SiteDb_Empty")
                .Options;

            var factory = GetInMemoryDbContextFactory("SiteDb_Empty", seedData: false);
            var repo = new SiteRepository(factory);

            // Act
            List<CmSiteMaster> sites = await repo.GetAll();

            // Assert
            Assert.IsNotNull(sites);
            Assert.AreEqual(0, sites.Count);
        }
    }
}
