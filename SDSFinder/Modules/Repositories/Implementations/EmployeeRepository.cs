using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContextFactory<CommonContext> commonContext;

    public EmployeeRepository( IDbContextFactory<CommonContext> _contextFactory)
    {
        commonContext = _contextFactory;
    }

    public async Task<CmEmployeeMaster?> GetBy(Expression<Func<CmEmployeeMaster, bool>> selector)
    {
        using (CommonContext context = await commonContext.CreateDbContextAsync())
        {
            CmEmployeeMaster? result = await context.CmEmployeeMasters.Where(selector).OrderBy(x => x.FullName).FirstOrDefaultAsync();
            return result;
        }
            
    }

    public async Task<List<CmEmployeeMaster>> GetLimitedListBy(Expression<Func<CmEmployeeMaster, bool>> selector, int take)
    {
        using (CommonContext context = await commonContext.CreateDbContextAsync())
        {
            List<CmEmployeeMaster> result = await context.CmEmployeeMasters.Where(selector).OrderBy(x => x.FullName).Take(take).ToListAsync();
            return result;
        }
    }
}
