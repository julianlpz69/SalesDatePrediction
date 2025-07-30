using API.DTOs;

namespace API.Interfaces
{
    public interface IShipperRepository
    {
        Task<List<ShipperDto>> GetAllShippersAsync();
    }
}
