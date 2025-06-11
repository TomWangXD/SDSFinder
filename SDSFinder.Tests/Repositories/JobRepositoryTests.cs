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
using SDSFinder.EFModels;
using MudBlazor;
using DevExpress.Blazor.Internal;

namespace SDSFinder.Tests.Repositories;

[TestClass]
public class JobRepositoryTests
{
    private Mock<IDbContextFactory<IndAppContext>> StubAppContextFactory { get; set; } = null!;
    private Mock<ISnackbar> Snackbar { get; set; } = null!;
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

        appContext.JobMsts.RemoveRange(appContext.JobMsts);

        await appContext.SaveChangesAsync();
    }

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
