

using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public class SiteRepository : ISiteRepository    
    {
        private readonly CommonContext _context;

        public SiteRepository(CommonContext context)
        {
            _context = context;
        }
        public async Task<List<CmSiteMaster>> GetAll(CommonContext context)
        {
                .OrderBy(x => x.SiteCode)
                .AsNoTracking()
                .ToListAsync();

            return sites;
        }
        public async Task<List<CmSiteMaster>> GetLimitedListBy(Expression<Func<CmSiteMaster, bool>> selector, int take, CommonContext context)
        {
            List<CmSiteMaster> sites = await context.CmSiteMasters.Where(selector).OrderBy(x => x.SiteCode).Take(take).ToListAsync();
            return sites;
        }
    }
}
