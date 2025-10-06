//using Indium.Common.DataTransferObjects.ActionItems;

using Indium.Components.ActionItems.DataTransferObjects;
//using Indium.Components.ActionItems.Models;
//using Indium.Components.ActionItems.Modules;
//using Stateless;


namespace SDSFinder.Modules.Services;

public interface IActionItemService
{
    Task CreateNewSDSActionItem(Document document);
    Task<Guid> CreateSDSActionItem(ActionItemModel actionItem);
}