using DevExpress.XtraPrinting;
using Indium.Common.DataTransferObjects.ActionItems;
using Indium.Common.Models.ActionItems;
using MudBlazor;
using System.Linq.Expressions;
using System.Text;
using Document = SDSFinder.EFModels.Document;
using User = SDSFinder.Shared.User;

namespace SDSFinder.Modules.Services;

public class ActionItemService : IActionItemService
{
    public IActionItemsClient _actionItemsClient;
    public IConfiguration _configuration;
    public User _user;
    public IAlertRecipientService _alertRecipientService;

    public ActionItemService(IActionItemsClient actionItemsClient, IConfiguration configuration, User user, IAlertRecipientService alertRecipientService)
    {
        _actionItemsClient = actionItemsClient;
        _configuration = configuration;
        _user = user;
        _alertRecipientService = alertRecipientService;
    }

    public async Task CreateNewSDSActionItem(Document document)
    {
        try
        {
            string basePath = _configuration.GetValue<string>("AppLocation")
                    ?? throw new ArgumentException("Cannot get base path");

            IReadOnlyList<AlertRecipient> recipients = await _alertRecipientService.GetAllAsync();

            List<AssignedToModel> assignedTo = recipients
                .Select(r => new AssignedToModel
                {
                    UserAssigned = r.UserId,
                    EscalationLevel = 0,
                })
                .ToList();

            ActionItemModel actionItemModel = new()
            {
                Title = $"New SDS {document.FileName} has been added.",
                Type = ActionItemTypes.OneTime,
                URL = $"{basePath}/SDSFinder/",
                Description = $"SDS {document.FileName} has been added to the {_configuration.GetValue<string>("Region")} environment of the SDS Finder App, please update Syteline Global Item records accordingly.",
                AssignedTo = assignedTo,
                CreatedBy = _user.Employee.Id

            };
            await CreateSDSActionItem(actionItemModel);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to create Action Item! Error: {ex}");
        }
    }

    public async Task<Guid> CreateSDSActionItem(ActionItemModel actionItem)
    {
        try
        {
            string appName = _configuration.GetValue<string>("ApplicationTitle")
                ?? throw new ArgumentException("Cannot get application title");

            actionItem.Application = appName;
            actionItem.CreatedBy = _user.Employee?.Id ?? 0;
            actionItem.Type = ActionItemTypes.OneTime;
            actionItem.IsEscalating = false;
            actionItem.StartDate = DateTime.UtcNow;
            actionItem.EndDate = null;

            List<ActionItemModel> actionItems = [actionItem];
            IEnumerable<ActionItemModel> newlyCreatedActionItems = await _actionItemsClient.CreateAsync(actionItems);

            return newlyCreatedActionItems.First().GKey;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to create Action Item! Error: {ex}");
        }
    }
}
