using SDSFinder.EFModels;

namespace SDSFinder.Modules.Services;

    public interface IJobService
    {
        Task<bool> ValidateJob(string jobNumber, string site);
        Task<string> ExpandJob(string jobNumber);
        Task<JobMst?> Get(string jobNumber, string site);
    }

