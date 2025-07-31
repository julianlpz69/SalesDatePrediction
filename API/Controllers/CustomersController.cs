using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }


    [HttpGet("predictions")]
    public async Task<ActionResult> GetPredictions(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "customername",
        [FromQuery] string sortOrder = "asc",
        [FromQuery] string? search = null)
    {
        var (data, total) = await _service.GetPredictionsAsync(page, pageSize, sortBy, sortOrder, search);

        return Ok(new
        {
            data,
            total
        });
    }
}
