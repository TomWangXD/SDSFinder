using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public interface ISiteRepository
    {
        Task<List<CmSiteMaster>> GetAll();
        
    }
}
