using SDSFinder.Modules;

namespace SDSFinder.Modules.Repositories;

public class JobRepository : IJobRepository
{
    private readonly IDbContextFactory<IndAppContext> _contextFactory;

    public JobRepository(IDbContextFactory<IndAppContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> ValidateJob(string jobNumber, string site)
    {
        await using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        return await context.JobMsts.AnyAsync(x => x.Job == jobNumber && x.SiteRef == site);
    }

    public async Task<JobMst?> Get(string jobNumber, string site)
    {
        await using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        return await context.JobMsts.Where(x => x.Job == jobNumber && x.SiteRef == site).FirstOrDefaultAsync();
    }

    public async Task<string> ExpandJob(string jobNumber)
    {
        await using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        if (string.IsNullOrEmpty(jobNumber)) return string.Empty;
        return ExpandKey.ExpandKeyByLength(jobNumber, 10, context);
    }
}

