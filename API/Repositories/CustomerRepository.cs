using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly StoreSampleContext _context;

    public CustomerRepository(StoreSampleContext context)
    {
        _context = context;
    }

    public async Task<(List<CustomerPredictionDto> Data, int Total)> GetPredictionsAsync(int page, int pageSize, string sortBy, string sortOrder, string? search)
    {
        var query = _context.Customers
            .Include(c => c.Orders)
            .Where(c => c.Orders.Count > 1);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(c => c.Companyname.Contains(search));
        }

        // Total antes de paginar
        var total = await query.CountAsync();

        // Ordenamiento dinámico
        query = sortBy.ToLower() switch
        {
            "lastorderdate" => sortOrder == "desc"
                ? query.OrderByDescending(c => c.Orders.Max(o => o.Orderdate))
                : query.OrderBy(c => c.Orders.Max(o => o.Orderdate)),

            _ => sortOrder == "desc"
                ? query.OrderByDescending(c => c.Companyname)
                : query.OrderBy(c => c.Companyname)
        };

        var customers = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var data = customers.Select(c =>
        {
            var orders = c.Orders.OrderBy(o => o.Orderdate).ToList();
            var intervals = orders.Zip(orders.Skip(1), (a, b) => (b.Orderdate - a.Orderdate).TotalDays).ToList();
            var avg = intervals.Any() ? intervals.Average() : 0;

            return new CustomerPredictionDto
            {
                CustomerId = c.Custid,
                CustomerName = c.Companyname,
                LastOrderDate = orders.Last().Orderdate,
                NextPredictedOrder = orders.Last().Orderdate.AddDays(avg)
            };
        }).ToList();

        return (data, total);
    }


}
