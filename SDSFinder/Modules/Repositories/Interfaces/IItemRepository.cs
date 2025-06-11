using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public interface IItemRepository
    {
        Task<bool> ValidateItem(string item, IND_APPContext context);
        Task<ItemGlbl?> Get(string item, IND_APPContext context);
        Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector, IND_APPContext context);
        Task<List<ItemGlbl>> GetLimitedListBy(Expression<Func<ItemGlbl, bool>> selector, int take, IND_APPContext context);
        Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector, IND_APPContext context);
    
    }
}
