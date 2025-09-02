using SDSFinder.Modules.Repositories.Interfaces;
using System;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories;

public class AlertRecipientRepository : IAlertRecipientRepository
{
    private readonly IDbContextFactory<SDSFinderContext> _dbFactory;
    public AlertRecipientRepository(IDbContextFactory<SDSFinderContext> db)
    {
        _dbFactory = db;
    }

    public async Task<AlertRecipient?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _dbFactory.CreateDbContextAsync(ct);
        return await db.AlertRecipients.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<AlertRecipient?> GetByUserIdAsync(int userId, CancellationToken ct = default)
    {
        await using var db = await _dbFactory.CreateDbContextAsync(ct);
        return await db.AlertRecipients.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);
    }

    public async Task<IReadOnlyList<AlertRecipient>> ListAsync(CancellationToken ct = default)
    {
        await using var db = await _dbFactory.CreateDbContextAsync(ct);
        return await db.AlertRecipients.AsNoTracking()
            .OrderBy(x => x.UserName)
            .ToListAsync(ct);
    }

    public async Task<bool> ExistsByUserIdAsync(int userId, CancellationToken ct = default)
    {
        await using var db = await _dbFactory.CreateDbContextAsync(ct);
        return await db.AlertRecipients.AsNoTracking()
            .AnyAsync(x => x.UserId == userId, ct);
    }

    public async Task AddAsync(AlertRecipient entity, CancellationToken ct = default)
    {
        await using var db = await _dbFactory.CreateDbContextAsync(ct);
        db.AlertRecipients.Add(entity);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(AlertRecipient entity, CancellationToken ct = default)
    {
        await using var db = await _dbFactory.CreateDbContextAsync(ct);
        db.AlertRecipients.Update(entity);
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        await using var db = await _dbFactory.CreateDbContextAsync(ct);
        var existing = await db.AlertRecipients.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (existing is null) return;
        db.AlertRecipients.Remove(existing);
        await db.SaveChangesAsync(ct);
    }
}
