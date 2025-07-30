using API.Models;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Repositories;

public class ShipperRepository : IShipperRepository
{
    private readonly StoreSampleContext _context;

    public ShipperRepository(StoreSampleContext context)
    {
        _context = context;
    }

    public async Task<List<ShipperDto>> GetAllShippersAsync()
    {
        return await _context.Shippers
            .Select(s => new ShipperDto
            {
                ShipperId = s.Shipperid,
                CompanyName = s.Companyname,
                Phone = s.Phone
            })
            .ToListAsync();
    }
}
