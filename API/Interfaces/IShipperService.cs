using API.DTOs;

namespace API.Interfaces
{
    public interface IShipperService
    {
        Task<List<ShipperDto>> GetAllShippersAsync();
    }

}
