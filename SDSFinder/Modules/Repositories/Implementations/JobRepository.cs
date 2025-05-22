
namespace SDSFinder.Modules.Repositories;

public class JobRepository : IJobRepository
{
    public async Task<bool> ValidateJob(string jobNumber, string site, IndAppContext context)
    {
        bool result = await context.JobMsts.AnyAsync(x => x.Job == jobNumber && x.SiteRef == site);
        return result;
    }
    public async Task<JobMst?> Get(string jobNumber,string site, IndAppContext context)
    {
        JobMst? value = await context.JobMsts.Where(x => x.Job == jobNumber && x.SiteRef == site).FirstOrDefaultAsync();
        return value;
    } 
}

