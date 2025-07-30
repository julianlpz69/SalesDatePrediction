namespace API.DTOs;

public class EmployeeDto
{
    public int Empid { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
}
