using API.DTOs;

namespace API.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<CustomerOrderDto>> GetOrdersByCustomerIdAsync(int customerId);
        Task<int> CreateOrderWithProductAsync(CreateOrderDto dto);
    }
}
