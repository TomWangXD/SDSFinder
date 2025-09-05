using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public interface IEmployeeService
{
    Task<List<CmEmployeeMaster>?> GetLimitedListBy(Expression<Func<CmEmployeeMaster, bool>> selector, int take);
}

