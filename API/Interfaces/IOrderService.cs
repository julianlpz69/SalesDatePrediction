using API.DTOs;

namespace API.Interfaces
{
    public interface IOrderService
    {
        Task<List<CustomerOrderDto>> GetOrdersByCustomerIdAsync(int customerId);
        Task<int> CreateOrderAsync(CreateOrderDto dto);
    }

}
