namespace API.DTOs;

public class CustomerOrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShipName { get; set; } = string.Empty;
    public decimal Freight { get; set; }
}
