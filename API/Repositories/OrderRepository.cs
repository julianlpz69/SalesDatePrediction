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

    public async Task<(List<CustomerOrderDto> Data, int Total)> GetOrdersByCustomerIdPagedAsync(
        int customerId,
        int page,
        int pageSize,
        string sortBy,
        string sortOrder)
    {
        var query = _context.Orders
            .Where(o => o.Custid == customerId)
            .Select(o => new CustomerOrderDto
            {
                OrderId = o.Orderid,
                RequiredDate = o.Requireddate,
                ShippedDate = o.Shippeddate,
                ShipName = o.Shipname,
                ShipAddress = o.Shipaddress,
                ShipCity = o.Shipcity
            });

        query = sortBy.ToLower() switch
        {
            "requireddate" => sortOrder == "desc" ? query.OrderByDescending(o => o.RequiredDate) : query.OrderBy(o => o.RequiredDate),
            "shippeddate" => sortOrder == "desc" ? query.OrderByDescending(o => o.ShippedDate) : query.OrderBy(o => o.ShippedDate),
            "shipname" => sortOrder == "desc" ? query.OrderByDescending(o => o.ShipName) : query.OrderBy(o => o.ShipName),
            "shipaddress" => sortOrder == "desc" ? query.OrderByDescending(o => o.ShipAddress) : query.OrderBy(o => o.ShipAddress),
            "shipcity" => sortOrder == "desc" ? query.OrderByDescending(o => o.ShipCity) : query.OrderBy(o => o.ShipCity),
            _ => sortOrder == "desc" ? query.OrderByDescending(o => o.OrderId) : query.OrderBy(o => o.OrderId)
        };

        var total = await query.CountAsync();
        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, total);
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
            Orderdate = dto.OrderDate ?? DateTime.Now,
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
