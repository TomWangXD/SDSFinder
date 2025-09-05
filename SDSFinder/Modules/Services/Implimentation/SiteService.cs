using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public class SiteService : ISiteService
{
    private readonly ISiteRepository _siteRepository;

    public SiteService(ISiteRepository siteRepository)
    {
        _siteRepository = siteRepository;
    }

    public Task<List<CmSiteMaster>> GetAll()
        => _siteRepository.GetAll();

    public Task<List<CmSiteMaster>> GetLimitedListBy(Expression<Func<CmSiteMaster, bool>> selector, int take)
        => _siteRepository.GetLimitedListBy(selector, take);
}
