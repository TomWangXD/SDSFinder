using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SDSFinder.Modules.Services.Interface;
public interface IAlertRecipientService
{
    Task<IReadOnlyList<AlertRecipient>> GetAllAsync(CancellationToken ct = default);
    Task<AlertRecipient?> GetByUserIdAsync(int userId, CancellationToken ct = default);

    /// <summary>Creates if not exists; updates UserName if it changed.</summary>
    Task<AlertRecipient> SubscribeAsync(int userId, string userName, CancellationToken ct = default);

    /// <summary>Removes a recipient by userId (no-op if not found).</summary>
    Task UnsubscribeAsync(int userId, CancellationToken ct = default);
}