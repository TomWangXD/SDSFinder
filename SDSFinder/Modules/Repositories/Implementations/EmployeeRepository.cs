using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContextFactory<CommonContext> _contextFactory;

    public EmployeeRepository(IDbContextFactory<CommonContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<CmEmployeeMaster>> GetLimitedListBy(Expression<Func<CmEmployeeMaster, bool>> selector, int take)
    {
        await using CommonContext context = await _contextFactory.CreateDbContextAsync();
        return await context.CmEmployeeMasters.Where(selector).OrderBy(x => x.FullName).Take(take).ToListAsync();
    }
}
