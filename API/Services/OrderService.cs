using API.DTOs;
using API.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class OrderService : IOrderService
{
    private readonly StoreSampleContext _context;
    private readonly IOrderRepository _repository;

    public OrderService(StoreSampleContext context, IOrderRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<List<CustomerOrderDto>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _repository.GetOrdersByCustomerIdAsync(customerId);
    }
    public async Task<int> CreateOrderAsync(CreateOrderDto dto)
    {
        var product = await _context.Products.FindAsync(dto.ProductId);
        if (product == null || product.Discontinued)
            throw new InvalidOperationException("Invalid or discontinued product.");

        var customerExists = await _context.Customers.AnyAsync(c => c.Custid == dto.CustomerId);
        if (!customerExists)
            throw new InvalidOperationException("Customer not found.");

        var shipperExists = await _context.Shippers.AnyAsync(s => s.Shipperid == dto.ShipperId);
        if (!shipperExists)
            throw new InvalidOperationException("Shipper not found.");

        return await _repository.CreateOrderWithProductAsync(dto);
    }
}
