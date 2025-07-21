using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public interface IEmployeeService
{
    Task<CmEmployeeMaster?> GetBy(Expression<Func<CmEmployeeMaster, bool>> selector);
}

