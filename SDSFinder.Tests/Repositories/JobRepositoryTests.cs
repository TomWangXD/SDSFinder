using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDSFinder.EFContexts;
using SDSFinder.Modules.Repositories;
using Moq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace SDSFinder.Tests.Repositories;

[TestClass]
public class JobRepositoryTests
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

        appContext.JobMsts.RemoveRange(appContext.JobMsts);

        await appContext.SaveChangesAsync();
    }

    public async Task<IND_APPContext> AddJobToMockDb(IND_APPContext appContext)
    {
        JobMst job = new()
        {
            Job = "J-1234567890",
            SiteRef = "BPD"
        };

        appContext.JobMsts.Add(job);

        await appContext.SaveChangesAsync();

        return appContext;
    }

    [TestMethod]
    public async Task ValidateJobSuccess()
    {
        IND_APPContext AppContext = StubAppContextFactory.Object.CreateDbContext();

        AppContext = await AddJobToMockDb(AppContext);
        JobRepository repo = new();

        var jobNumber = "J-1234567890";
        var site = "BPD";

        var validationResult = await repo.ValidateJob(jobNumber, site, AppContext);
        Assert.IsTrue(validationResult);
    }

    [TestMethod]
    public async Task ValidateJobFailure()
    {
        IND_APPContext AppContext = StubAppContextFactory.Object.CreateDbContext();

        AppContext = await AddJobToMockDb(AppContext);
        JobRepository repo = new();

        var jobNumber = "n/a";
        var site = "n/a";

        var validationResult = await repo.ValidateJob(jobNumber, site, AppContext);
        Assert.IsFalse(validationResult);
    }

    [TestMethod]
    public async Task ValidateJobPartialMatchSiteFailure()
    {
        IND_APPContext AppContext = StubAppContextFactory.Object.CreateDbContext();

        AppContext = await AddJobToMockDb(AppContext);
        JobRepository repo = new();

        var jobNumber = "n/a";
        var site = "BPD";

        var validationResult = await repo.ValidateJob(jobNumber, site, AppContext);
        Assert.IsFalse(validationResult);
    }

    [TestMethod]
    public async Task ValidateJobPartialMatchJobFailure()
    {
        IND_APPContext AppContext = StubAppContextFactory.Object.CreateDbContext();

        AppContext = await AddJobToMockDb(AppContext);
        JobRepository repo = new();

        var jobNumber = "J-1234567890";
        var site = "n/a";

        var validationResult = await repo.ValidateJob(jobNumber, site, AppContext);
        Assert.IsFalse(validationResult);
    }
    [TestMethod]
    public async Task GetJobReturnsValidResult()
    {
        IndAppContext AppContext = StubAppContextFactory.Object.CreateDbContext();

        AppContext = await AddJobToMockDb(AppContext);
        JobRepository repo = new();

        var jobNumber = "J-1234567890";
        var site = "BPD";

        var getResult = await repo.Get(jobNumber, site, AppContext);
        Assert.IsNotNull(getResult);
        Assert.AreEqual(getResult.Job, jobNumber);

    }
}
