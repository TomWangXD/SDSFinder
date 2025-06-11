using Indium.Common.EFContexts;
using Indium.Common.EFModels;
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
    public class SiteRepositoryTests
    {        
        private CommonContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CommonContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new CommonContext(options);

            // Seed test data
            context.CmSiteMasters.AddRange(
                new CmSiteMaster { Id = 1, SiteCode = "A001", SiteName = "Site A", IsActive = true, TimeZone = "UTC"},
                new CmSiteMaster { Id = 2, SiteCode = "B002", SiteName = "Site B", IsActive = true, TimeZone = "UTC"}
            );
            context.SaveChanges();

            return context;
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnSitesOrderedBySiteCode()
        {
            // Arrange
            var context = GetInMemoryDbContext("SiteDb_GetAll");
            var repo = new SiteRepository(context);

            // Act
            List<CmSiteMaster> sites = await repo.GetAll(context);

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

            using var context = new CommonContext(options); // No seeding
            var repo = new SiteRepository(context);

            // Act
            List<CmSiteMaster> sites = await repo.GetAll(context);

            // Assert
            Assert.IsNotNull(sites);
            Assert.AreEqual(0, sites.Count);
        }
    }
}
