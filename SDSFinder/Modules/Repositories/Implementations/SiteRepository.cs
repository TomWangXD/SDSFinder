
namespace SDSFinder.Modules.Repositories
{
    public class SiteRepository : ISiteRepository    
    {
        private readonly CommonContext _context;

        public SiteRepository(CommonContext context)
        {
            _context = context;
        }
        public async Task<List<CmSiteMaster>> GetAll()
        {
            List<CmSiteMaster> sites = await _context.CmSiteMasters
                .OrderBy(x => x.SiteCode)
                .AsNoTracking()
                .ToListAsync();

            return sites;
        }
    }
}
