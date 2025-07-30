using API.DTOs;
using API.Models;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreSampleContext _context;

    public OrderRepository(StoreSampleContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerOrderDto>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => o.Custid == customerId)
            .Select(o => new CustomerOrderDto
            {
                OrderId = o.Orderid,
                OrderDate = o.Orderdate,
                ShipName = o.Shipname,
                Freight = o.Freight
            })
            .ToListAsync();
    }

    public async Task<int> CreateOrderWithProductAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            Custid = dto.CustomerId,
            Empid = dto.EmployeeId,
            Shipperid = dto.ShipperId,
            Shipname = dto.ShipName,
            Shipaddress = dto.ShipAddress,
            Shipcity = dto.ShipCity,
            Shipcountry = dto.ShipCountry,
            Freight = dto.Freight,
            Orderdate = DateTime.Now,
            Requireddate = dto.RequiredDate ?? DateTime.Now.AddDays(7),
            Shippeddate = dto.ShippedDate
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var detail = new OrderDetail
        {
            Orderid = order.Orderid,
            Productid = dto.ProductId,
            Unitprice = dto.UnitPrice,
            Qty = dto.Quantity,
            Discount = dto.Discount
        };

        _context.OrderDetails.Add(detail);
        await _context.SaveChangesAsync();

        return order.Orderid;
    }
}
