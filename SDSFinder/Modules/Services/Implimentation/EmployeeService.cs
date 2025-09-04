using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<CmEmployeeMaster?> GetBy(Expression<Func<CmEmployeeMaster, bool>> selector)
    {
        return await _employeeRepository.GetBy(selector);
    }

    public async Task<IEnumerable<CmEmployeeMaster>?> GetLimitedListBy(Expression<Func<CmEmployeeMaster, bool>> selector, int take)
    {
        return await _employeeRepository.GetLimitedListBy(selector, take);
    }
}