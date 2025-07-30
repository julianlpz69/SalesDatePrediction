using API.Models;

namespace API.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Order>> GetAllOrdersWithCustomerAsync();
    }
}
