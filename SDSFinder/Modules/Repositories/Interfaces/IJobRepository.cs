using SDSFinder.EFContexts;
using SDSFinder.EFModels;
using System.Threading.Tasks;

namespace SDSFinder.Modules.Repositories;

public interface IJobRepository
{
    Task<bool> ValidateJob(string jobNumber, string site, IndAppContext context);
    Task<JobMst?> Get(string jobNumber, string site, IndAppContext context);
}

