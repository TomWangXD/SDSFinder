using SDSFinder.EFContexts;
using Microsoft.EntityFrameworkCore;
using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public class SiteService : ISiteService
{
    private readonly ISiteRepository _siteRepository;
    private readonly IDbContextFactory<CommonContext> _contextFactory;

    public SiteService(ISiteRepository siteRepository, IDbContextFactory<CommonContext> contextFactory)
    {
        _siteRepository = siteRepository;
        _contextFactory = contextFactory;
    }

    public async Task<List<CmSiteMaster>> GetAll()
    {
        using CommonContext context = await _contextFactory.CreateDbContextAsync();

        var sites = await _siteRepository.GetAll(context);

        return sites;
    }

    public async Task<List<CmSiteMaster>> GetLimitedListBy(Expression<Func<CmSiteMaster, bool>> selector, int take)
    {
        using CommonContext context = _contextFactory.CreateDbContext();

        var sites = await _siteRepository.GetLimitedListBy(selector, take, context);

        return sites;
    }
}
