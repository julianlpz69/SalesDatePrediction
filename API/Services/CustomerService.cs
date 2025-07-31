using API.DTOs;
using API.Interfaces;

namespace API.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }
    public async Task<(List<CustomerPredictionDto> Data, int Total)> GetPredictionsAsync(int page, int pageSize, string sortBy, string sortOrder, string? search)
    {
        return await _repository.GetPredictionsAsync(page, pageSize, sortBy, sortOrder, search);
    }


}
