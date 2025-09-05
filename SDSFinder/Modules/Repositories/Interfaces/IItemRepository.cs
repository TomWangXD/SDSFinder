using SDSFinder.EFContexts;
using SDSFinder.EFModels;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories
{
    public interface IItemRepository
    {
        Task<bool> ValidateItem(string item);
        Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector);
        Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector);
    }
}
