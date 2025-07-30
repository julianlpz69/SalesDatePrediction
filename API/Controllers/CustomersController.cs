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
    public async Task<ActionResult<List<CustomerPredictionDto>>> GetPredictions()
    {
        var result = await _service.GetPredictionsAsync();
        return Ok(result);
    }
}
