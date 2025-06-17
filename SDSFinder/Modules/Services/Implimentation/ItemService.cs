using SDSFinder.EFContexts;
using SDSFinder.EFModels;
using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace SDSFinder.Modules.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IDbContextFactory<IndAppContext> _contextFactory;

    public ItemService(IItemRepository itemRepository, IDbContextFactory<IndAppContext> contextFactory)
    {
        _itemRepository = itemRepository;
        _contextFactory = contextFactory;
    }
    public async Task<bool> ValidateItem(string item)
    {
        using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        bool result = await _itemRepository.ValidateItem(item, context);
        return result;
    }


    public async Task<ItemGlbl?> GetBy(Expression<Func<ItemGlbl, bool>> selector)
    { 
        using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        ItemGlbl? result = await _itemRepository.GetBy(selector, context);
        return result;
    }
    public async Task<List<ItemGlbl>> GetListBy(Expression<Func<ItemGlbl, bool>> selector)
    {
        using IndAppContext context = await _contextFactory.CreateDbContextAsync();
        List<ItemGlbl> result = await _itemRepository.GetListBy(selector, context);
        return result;
    }
}
