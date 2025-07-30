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

    public async Task<List<CustomerPredictionDto>> GetPredictionsAsync()
    {
        var orders = await _repository.GetAllOrdersWithCustomerAsync();

        return orders
            .GroupBy(o => o.Custid)
            .Where(g => g.Count() > 1)
            .Select(g =>
            {
                var sortedOrders = g.OrderBy(o => o.Orderdate).ToList();
                var diffs = new List<int>();

                for (int i = 1; i < sortedOrders.Count; i++)
                {
                    var daysDiff = (sortedOrders[i].Orderdate - sortedOrders[i - 1].Orderdate).TotalDays;
                    diffs.Add((int)daysDiff);
                }

                var averageDiff = diffs.Any() ? diffs.Average() : 0;

                return new CustomerPredictionDto
                {
                    CustomerName = g.First().Cust!.Companyname,
                    LastOrderDate = sortedOrders.Last().Orderdate,
                    NextPredictedOrder = sortedOrders.Last().Orderdate.AddDays(averageDiff)
                };
            })
            .ToList();
    }


}
