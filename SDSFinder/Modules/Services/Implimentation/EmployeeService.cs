using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public Task<List<CmEmployeeMaster>> GetLimitedListBy(Expression<Func<CmEmployeeMaster, bool>> selector, int take)
        => _employeeRepository.GetLimitedListBy(selector, take);
}