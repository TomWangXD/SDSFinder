using SDSFinder.EFModels;
using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    public Task<bool> ValidateItem(string item)
        => _itemRepository.ValidateItem(item);

    public Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector)
        => _itemRepository.GetBy(selector);

    public Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector)
        => _itemRepository.GetListBy(selector);
}
