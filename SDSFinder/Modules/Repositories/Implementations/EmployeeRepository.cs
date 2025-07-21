using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public async Task<CmEmployeeMaster?> GetBy(Expression<Func<CmEmployeeMaster, bool>> selector, CommonContext context)
    {
        CmEmployeeMaster? result = await context.CmEmployeeMasters.Where(selector).OrderBy(x => x.FullName).FirstOrDefaultAsync();
        return result;
    }
}
