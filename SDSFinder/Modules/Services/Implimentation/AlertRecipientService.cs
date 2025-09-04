using SDSFinder.Modules.Repositories.Interfaces;
using SDSFinder.Modules.Services;

namespace SDSFinder.Modules.Services.Implimentation;


public class AlertRecipientService : IAlertRecipientService
{
    private readonly IAlertRecipientRepository _repo;
    public AlertRecipientService(IAlertRecipientRepository repo) => _repo = repo;

    public Task<IReadOnlyList<AlertRecipient>> GetAllAsync(CancellationToken ct = default) =>
        _repo.ListAsync(ct);

    public Task<AlertRecipient?> GetByUserIdAsync(int userId, CancellationToken ct = default) =>
        _repo.GetByUserIdAsync(userId, ct);

    public async Task<AlertRecipient> SubscribeAsync(int userId, string userName, CancellationToken ct = default)
    {
        var existing = await _repo.GetByUserIdAsync(userId, ct);
        if (existing is not null)
        {
            if (!string.Equals(existing.UserName, userName, StringComparison.Ordinal))
            {
                existing.UserName = userName;
                await _repo.UpdateAsync(existing, ct);
            }
            return existing;
        }

        var entity = new AlertRecipient { UserId = userId, UserName = userName };
        await _repo.AddAsync(entity, ct);
        return entity;
    }

    public async Task UnsubscribeAsync(int userId, CancellationToken ct = default)
    {
        var existing = await _repo.GetByUserIdAsync(userId, ct);
        if (existing is null) return;
        await _repo.DeleteAsync(existing.Id, ct);
    }
}
