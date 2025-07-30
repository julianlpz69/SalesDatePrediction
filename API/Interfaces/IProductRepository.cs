using API.DTOs;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProductsAsync();
    }
}
