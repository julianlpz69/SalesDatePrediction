using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface ICustomerRepository
    {
        Task<(List<CustomerPredictionDto> Data, int Total)> GetPredictionsAsync(int page, int pageSize, string sortBy, string sortOrder, string? search);
    }
}
