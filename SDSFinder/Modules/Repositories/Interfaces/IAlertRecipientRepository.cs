using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories.Interfaces;

public interface IAlertRecipientRepository
{
    Task<AlertRecipient?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<AlertRecipient?> GetByUserIdAsync(int userId, CancellationToken ct = default);
    Task<IReadOnlyList<AlertRecipient>> ListAsync(CancellationToken ct = default);

    Task<bool> ExistsByUserIdAsync(int userId, CancellationToken ct = default);

    Task AddAsync(AlertRecipient entity, CancellationToken ct = default);
    Task UpdateAsync(AlertRecipient entity, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}