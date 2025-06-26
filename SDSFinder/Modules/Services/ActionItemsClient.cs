using Indium.Common.DataTransferObjects.ActionItems;
using Indium.Common.Modules.ActionItems;

namespace SDSFinder.Modules.Services;

public class ActionItemsClient : IActionItemsClient
{
    public ApiActionItems _actionItemsService;

    public ActionItemsClient(ApiActionItems actionItemsApi)
    {
        _actionItemsService = actionItemsApi;
    }

    public async Task<IEnumerable<ActionItemModel>> CreateAsync(IEnumerable<ActionItemModel> items)
    {
        try
        {
            return await _actionItemsService.CreateAsync(items);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<bool>> Delete(IEnumerable<Guid> GKeys)
    {
        try
        {
            return await _actionItemsService.Delete(GKeys);
        }
        catch (Exception)
        {
            throw;
        }
    }
}