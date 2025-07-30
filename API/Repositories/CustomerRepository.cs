using API.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly StoreSampleContext _context;

    public CustomerRepository(StoreSampleContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllOrdersWithCustomerAsync()
    {
        return await _context.Orders
            .Include(o => o.Cust)
            .Where(o => o.Custid != null && o.Cust != null)
            .OrderBy(o => o.Orderdate)
            .ToListAsync();
    }


}
