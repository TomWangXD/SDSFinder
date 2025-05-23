using Indium.Common.EFContexts;
using Indium.Common.EFModels;
using SDSFinder.EFContexts;
using SDSFinder.EFModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SDSFinder.Modules.Repositories;
using SDSFinder.Modules.Repositories.Implementations;
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

        [TestInitialize]
        public void Init()
        {
            // IND_APPContext
            StubAppContextFactory.Setup(f => f.CreateDbContext())
            StubAppContextFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await using var appContext = StubAppContextFactory.Object.CreateDbContext();

            appContext.ItemGlbls.RemoveRange(appContext.ItemGlbls);

            await appContext.SaveChangesAsync();
        }

        {
            {

            appContext.ItemGlbls.Add(item);

            await appContext.SaveChangesAsync();

            return appContext;
        }

    }
}
