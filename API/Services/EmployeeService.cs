using API.DTOs;
using API.Interfaces;

namespace API.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeDto>> GetAllAsync()
    {
        return await _repository.GetAllEmployeesAsync();
    }
}
