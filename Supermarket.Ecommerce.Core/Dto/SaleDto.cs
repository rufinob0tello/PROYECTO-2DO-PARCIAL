namespace Supermarket.Ecommerce.Core.Dto;

public class SaleDto
{
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleDetailDto> Details { get; set; } = new();
}