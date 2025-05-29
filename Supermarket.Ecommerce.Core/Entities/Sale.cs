namespace Supermarket.Ecommerce.Core.Entities;

public class Sale : EntityBase
{
    public int CustomerId { get; set; }
    public decimal Total { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int EmployeeId { get; set; }
}
