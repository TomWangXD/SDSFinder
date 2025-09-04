using System.Linq.Expressions;

namespace SDSFinder.Modules.Repositories;

public interface IEmployeeRepository
{
    Task<CmEmployeeMaster?> GetBy(Expression<Func<CmEmployeeMaster, bool>> selector);
    Task<List<CmEmployeeMaster>> GetLimitedListBy(Expression<Func<CmEmployeeMaster, bool>> selector, int take);
}
