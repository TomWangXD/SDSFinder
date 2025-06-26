using Indium.Common.DataTransferObjects.ActionItems;

namespace SDSFinder.Modules.Services;

public interface IActionItemsClient
{
    Task<IEnumerable<ActionItemModel>> CreateAsync(IEnumerable<ActionItemModel> items);
    Task<IEnumerable<bool>> Delete(IEnumerable<Guid> GKeys);
}
