using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public async Task<bool> ValidateItem(string item, IndAppContext context)
        {
            bool result = await context.ItemGlbls.AnyAsync(x => x.Item == item);
            return result;
        }

        {
            ItemGlbl? value = await context.ItemGlbls.Where(x => x.Item == item).FirstOrDefaultAsync();
            return value;
        }
        {
            ItemGlbl? result = await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).FirstOrDefaultAsync();
            return result;
        }

        {
            List<ItemGlbl> result = await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).Take(take).ToListAsync();
            return result;
        }

        {
            List<ItemGlbl> result = await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).ToListAsync();
            return result;
        }
    }
   
}
