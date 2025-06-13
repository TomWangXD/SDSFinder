using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

    public interface ISiteService
    {
        Task<List<CmSiteMaster>> GetAll();
        Task<List<CmSiteMaster>> GetLimitedListBy(Expression<Func<CmSiteMaster, bool>> selector, int take);
    }
