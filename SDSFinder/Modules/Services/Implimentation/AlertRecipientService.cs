using SDSFinder.Modules.Repositories.Interfaces;
using SDSFinder.Modules.Services;

namespace SDSFinder.Modules.Services.Implimentation;

public class AlertRecipientService : IAlertRecipientService
{
    private readonly IAlertRecipientRepository _repo;
    public AlertRecipientService(IAlertRecipientRepository repo) => _repo = repo;

    public Task<IReadOnlyList<AlertRecipient>> GetAllAsync(CancellationToken ct = default) =>
        _repo.ListAsync(ct);

    public async Task<bool> AddAsync(int userId, string userName, CancellationToken ct = default)
    {
        if (await _repo.ExistsByUserIdAsync(userId, ct)) return false;

        await _repo.AddAsync(new AlertRecipient { UserId = userId, UserName = userName }, ct);

        return true;
    }

    public Task DeleteAsync(int userId, CancellationToken ct = default) =>
        _repo.DeleteByUserIdAsync(userId, ct);
}
