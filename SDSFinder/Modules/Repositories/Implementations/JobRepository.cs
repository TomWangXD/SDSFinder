using Indium.Infor.EFContexts;

namespace SDSFinder.Modules.Repositories;

public class JobRepository : IJobRepository
{
    public async Task<bool> ValidateJob(string jobNumber, string site, IND_APPContext context)
    {
        bool result = await context.JobMsts.AnyAsync(x => x.Job == jobNumber && x.SiteRef == site);
        return result;
    }
}