using API.DTOs;
using API.Interfaces;

namespace API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        return await _repository.GetAllProductsAsync();
    }
}
