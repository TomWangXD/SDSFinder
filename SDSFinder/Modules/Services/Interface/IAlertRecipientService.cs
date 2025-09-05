using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SDSFinder.Modules.Services;
public interface IAlertRecipientService
{
    Task<IReadOnlyList<AlertRecipient>> GetAllAsync(CancellationToken ct = default);
    Task<bool> AddAsync(int userId, string userName, CancellationToken ct = default);
    Task DeleteAsync(int userId, CancellationToken ct = default);
}