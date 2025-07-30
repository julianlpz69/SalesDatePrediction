using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShippersController : ControllerBase
{
    private readonly IShipperService _service;

    public ShippersController(IShipperService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ShipperDto>>> GetAll()
    {
        var shippers = await _service.GetAllShippersAsync();
        return Ok(shippers);
    }
}
