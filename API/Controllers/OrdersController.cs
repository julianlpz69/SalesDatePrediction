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
    public async Task<ActionResult<object>> GetOrdersByCustomer(
        int customerId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "orderid",
        [FromQuery] string sortOrder = "asc")
    {
        var result = await _service.GetOrdersByCustomerPagedAsync(customerId, page, pageSize, sortBy, sortOrder);

        if (!result.Data.Any())
            return NotFound();

        return Ok(new { data = result.Data, total = result.Total });
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var orderId = await _service.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = orderId }, new { orderId });
    }

}
