using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly IDbContextFactory<CommonContext> _contextFactory;

        public SiteRepository(IDbContextFactory<CommonContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<CmSiteMaster>> GetAll()
        {
            await using CommonContext context = await _contextFactory.CreateDbContextAsync();
            return await context.CmSiteMasters
                .OrderBy(x => x.SiteCode)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<CmSiteMaster>> GetLimitedListBy(Expression<Func<CmSiteMaster, bool>> selector, int take)
        {
            await using CommonContext context = await _contextFactory.CreateDbContextAsync();
            return await context.CmSiteMasters.Where(selector).OrderBy(x => x.SiteCode).Take(take).ToListAsync();
        }
    }
}
