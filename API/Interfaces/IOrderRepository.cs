using API.DTOs;

namespace API.Interfaces
{
    public interface IOrderRepository
    {
        Task<(List<CustomerOrderDto> Data, int Total)> GetOrdersByCustomerIdPagedAsync(
            int customerId,
            int page,
            int pageSize,
            string sortBy,
            string sortOrder);
        Task<int> CreateOrderWithProductAsync(CreateOrderDto dto);
    }
}
