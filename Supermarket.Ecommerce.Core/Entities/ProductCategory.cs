namespace Supermarket.Ecommerce.Core.Entities;

public class ProductCategory : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int EmployeeId { get; set; }  
}