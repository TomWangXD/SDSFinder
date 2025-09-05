using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDbContextFactory<IndAppContext> _contextFactory;

        public ItemRepository(IDbContextFactory<IndAppContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> ValidateItem(string item)
        {
            await using IndAppContext context = await _contextFactory.CreateDbContextAsync();
            return await context.ItemGlbls.AnyAsync(x => x.Item == item);
        }

        public async Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector)
        {
            await using IndAppContext context = await _contextFactory.CreateDbContextAsync();
            return await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).FirstOrDefaultAsync();
        }

        public async Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector)
        {
            await using IndAppContext context = await _contextFactory.CreateDbContextAsync();
            return await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).ToListAsync();
        }
    }
   
}
