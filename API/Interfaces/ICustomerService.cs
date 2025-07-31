using API.DTOs;

namespace API.Interfaces
{
    public interface ICustomerService
    {
        Task<(List<CustomerPredictionDto> Data, int Total)> GetPredictionsAsync(int page, int pageSize, string sortBy, string sortOrder, string? search);
    }
}
