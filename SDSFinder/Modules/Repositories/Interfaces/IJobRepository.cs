using Indium.Infor.EFContexts;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public interface IJobRepository
    {
        Task<bool> ValidateJob(string jobNumber, string site, IND_APPContext context);

    }
}
