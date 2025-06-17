using SDSFinder.EFModels;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Services
{
    public interface IItemService
    {
        Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector);
        Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector);
        Task<bool> ValidateItem(string item);
    }
}
