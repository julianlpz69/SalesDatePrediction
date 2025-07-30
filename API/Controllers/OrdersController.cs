using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet("{customerId}/orders")]
    public async Task<ActionResult<List<CustomerOrderDto>>> GetOrdersByCustomer(int customerId)
    {
        var orders = await _service.GetOrdersByCustomerIdAsync(customerId);
        if (orders == null || !orders.Any())
            return NotFound();

        return Ok(orders); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var orderId = await _service.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = orderId }, new { orderId });
    }

}
