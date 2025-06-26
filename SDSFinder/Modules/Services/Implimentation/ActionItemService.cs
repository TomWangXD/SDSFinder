using DevExpress.XtraPrinting;
using Indium.Common.DataTransferObjects.ActionItems;
using Indium.Common.Models.ActionItems;
using MudBlazor;
using System.Linq.Expressions;
using System.Text;
using Document = SDSFinder.EFModels.Document;

namespace SDSFinder.Modules.Services;

public class ActionItemService : IActionItemService
{
    public IActionItemsClient _actionItemsClient;
    public IConfiguration _configuration;
    public User _user;

    public ActionItemService(IActionItemsClient actionItemsClient, IConfiguration configuration, User user)
    {
        _actionItemsClient = actionItemsClient;
        _configuration = configuration;
        _user = user;
    }
    public async Task CreateNewSDSActionItem(Document document)
    {
        try
        {
            string assignedTo = _configuration.GetValue<string>("AuthorizationPolicyGroups:Open Admin")
                   ?? throw new ArgumentException("Cannot get Admin AD Group");
            ActionItemModel actionItemModel = new() 
            { 
                Title = $"New SDS {document.FileName} has been added.",
                Description = $"SDS {document.FileName} has been added to the {_configuration.GetValue<string>("Region")} environment of the SDS Finder App, please update Syteline Global Item records accordingly.", 
            };

        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to create Action Item! Error: {ex}");
        }
    }
}
