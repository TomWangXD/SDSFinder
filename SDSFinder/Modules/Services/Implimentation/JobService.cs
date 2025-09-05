using SDSFinder.EFModels;

using SDSFinder.Modules.Repositories;

namespace SDSFinder.Modules.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public Task<bool> ValidateJob(string jobNumber, string site)
        => _jobRepository.ValidateJob(jobNumber, site);

    public Task<string> ExpandJob(string jobNumber)
        => _jobRepository.ExpandJob(jobNumber);

    public Task<JobMst?> Get(string jobNumber, string site)
        => _jobRepository.Get(jobNumber, site);
}
