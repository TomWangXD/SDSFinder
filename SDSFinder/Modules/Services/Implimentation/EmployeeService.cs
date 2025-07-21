using SDSFinder.Modules.Repositories;
using System.Linq.Expressions;

namespace SDSFinder.Modules.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDbContextFactory<CommonContext> _contextFactory;
    private readonly IDbContextFactory<SDSFinderContext> _sdsFinderContextFactory;

    public EmployeeService(IEmployeeRepository employeeRepository, IDbContextFactory<CommonContext> contextFactory, IDbContextFactory<SDSFinderContext> sdsFinderContextFactory)
    {
        _employeeRepository = employeeRepository;
        _contextFactory = contextFactory;
        _sdsFinderContextFactory = sdsFinderContextFactory;
    }

    public async Task<CmEmployeeMaster?> GetBy(Expression<Func<CmEmployeeMaster, bool>> selector)
    {
        using CommonContext context = _contextFactory.CreateDbContext();
        return await _employeeRepository.GetBy(selector, context);
    }
}