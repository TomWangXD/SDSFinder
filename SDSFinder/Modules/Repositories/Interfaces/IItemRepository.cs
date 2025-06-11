using SDSFinder.EFContexts;
using SDSFinder.EFModels;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public interface IItemRepository
    {
        Task<bool> ValidateItem(string item, IndAppContext context);
        Task<ItemGlbl?> Get(string item, IndAppContext context);
        Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector, IndAppContext context);
        Task<List<ItemGlbl>> GetLimitedListBy(Expression<Func<ItemGlbl, bool>> selector, int take, IndAppContext context);
        Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector, IndAppContext context);
    
    }
}
