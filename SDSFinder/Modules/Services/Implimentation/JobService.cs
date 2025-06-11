using SDSFinder.EFContexts;
using SDSFinder.EFModels;
using SDSFinder.Modules;

using SDSFinder.Modules.Repositories;

namespace SDSFinder.Modules.Services;

public class JobService : IJobService   
{
    private readonly IJobRepository _jobRepository;
    private readonly IDbContextFactory<IndAppContext> _contextFactory;

    public JobService(IJobRepository jobRepository, IDbContextFactory<IndAppContext> contextFactory)
    {
        _jobRepository = jobRepository;
        _contextFactory = contextFactory;
    }

    public async Task<bool> ValidateJob(string jobNumber, string site)
    {
        using IndAppContext context = await _contextFactory.CreateDbContextAsync();

        jobNumber = await ExpandJob(jobNumber);

        return await _jobRepository.ValidateJob(jobNumber, site, context);
    }

    public async Task<string> ExpandJob(string jobNumber)
    {
        using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        string result = string.Empty;
        if (!string.IsNullOrEmpty(jobNumber))
        {
            result = ExpandKey.ExpandKeyByLength(jobNumber, 10, context);
        }
        return result;
    }
    public async Task<JobMst?> Get(string jobNumber, string site)
    {
        using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        JobMst? value = await _jobRepository.Get(jobNumber, site,context);
        return value;
    }
}
