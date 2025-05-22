using Indium.Infor.EFContexts;
using Indium.Infor.EFModels;
using Microsoft.EntityFrameworkCore.Internal;
using SDSFinder.Modules.Repositories.Interfaces;
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

        public async Task<ItemGlbl?> Get(string item, IND_APPContext context)
        {
            ItemGlbl? value = await context.ItemGlbls.Where(x => x.Item == item).FirstOrDefaultAsync();
            return value;
        }
        public async Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector, IND_APPContext context)
        {
            ItemGlbl? result = await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<ItemGlbl>> GetLimitedListBy(Expression<Func<ItemGlbl, bool>> selector, int take, IND_APPContext context)
        {
            List<ItemGlbl> result = await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).Take(take).ToListAsync();
            return result;
        }

        public async Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector, IND_APPContext context)
        {
            List<ItemGlbl> result = await context.ItemGlbls.Where(selector).OrderBy(x => x.Item).ToListAsync();
            return result;
        }
    }
   
}
