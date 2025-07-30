namespace API.DTOs;

public class CreateOrderDto
{
    public int? CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ShipperId { get; set; }
    public string ShipName { get; set; } = null!;
    public string ShipAddress { get; set; } = null!;
    public string ShipCity { get; set; } = null!;
    public string ShipCountry { get; set; } = null!;
    public decimal Freight { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }

    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public decimal Discount { get; set; }
}
