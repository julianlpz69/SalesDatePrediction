using API.Models;
using API.DTOs;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly StoreSampleContext _context;

    public EmployeeRepository(StoreSampleContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
    {
        return await _context.Employees
            .Select(e => new EmployeeDto
            {
                Empid = e.Empid,
                Firstname = e.Firstname,
                Lastname = e.Lastname,
                Title = e.Title,
                HireDate = e.Hiredate,
                City = e.City,
                Country = e.Country
            })
            .ToListAsync();
    }

}
