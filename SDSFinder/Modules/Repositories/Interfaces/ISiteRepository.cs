using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public interface ISiteRepository
    {
        Task<List<CmSiteMaster>> GetAll(CommonContext context);
        Task<List<CmSiteMaster>> GetLimitedListBy(Expression<Func<CmSiteMaster, bool>> selector, int take, CommonContext context);
    }
}
