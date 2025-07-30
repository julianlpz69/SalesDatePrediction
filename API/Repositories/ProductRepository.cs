using API.DTOs;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreSampleContext _context;

    public ProductRepository(StoreSampleContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Select(p => new ProductDto
            {
                ProductId = p.Productid,
                ProductName = p.Productname,
                UnitPrice = p.Unitprice,
                Discontinued = p.Discontinued,
                CategoryName = p.Category.Categoryname,
                SupplierName = p.Supplier.Companyname
            })
            .ToListAsync();
    }
}
