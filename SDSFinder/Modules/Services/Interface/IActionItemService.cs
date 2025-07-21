using Indium.Common.DataTransferObjects.ActionItems;

namespace SDSFinder.Modules.Services;

public interface IActionItemService
{
    Task CreateNewSDSActionItem(Document document);
    Task<Guid> CreateSDSActionItem(ActionItemModel actionItem);
}

