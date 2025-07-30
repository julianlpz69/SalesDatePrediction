using API.DTOs;

namespace API.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerPredictionDto>> GetPredictionsAsync();
    }
}
