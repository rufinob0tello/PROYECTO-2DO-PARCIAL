namespace Supermarket.Ecommerce.Core.Entities;

public class Supplier : EntityBase
{
    public string Name { get; set; }
    public string ContactPhone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int EmployeeId { get; set; }
}