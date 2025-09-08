using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories.Interfaces;

public interface IAlertRecipientRepository
{
    Task<IReadOnlyList<AlertRecipient>> ListAsync(CancellationToken ct = default);
    Task<bool> ExistsByUserIdAsync(int userId, CancellationToken ct = default);
    Task AddAsync(AlertRecipient entity, CancellationToken ct = default);
    Task DeleteByUserIdAsync(int userId, CancellationToken ct = default);
}