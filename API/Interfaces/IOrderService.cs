using API.DTOs;

namespace API.Interfaces
{
    public interface IOrderService
    {
        Task<(List<CustomerOrderDto> Data, int Total)> GetOrdersByCustomerPagedAsync(
                int customerId,
                int page,
                int pageSize,
                string sortBy,
                string sortOrder); 
        Task<int> CreateOrderAsync(CreateOrderDto dto);
    }

}
