namespace API.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public bool Discontinued { get; set; }
    public string CategoryName { get; set; } = null!;
    public string SupplierName { get; set; } = null!;
}
