using API.DTOs;
using API.Interfaces;

namespace API.Services;

public class ShipperService : IShipperService
{
    private readonly IShipperRepository _repository;

    public ShipperService(IShipperRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ShipperDto>> GetAllShippersAsync()
    {
        return await _repository.GetAllShippersAsync();
    }
}
