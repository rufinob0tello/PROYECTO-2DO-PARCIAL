namespace Supermarket.Ecommerce.Core.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductCategoryId { get; set; }  
    public int EmployeeId { get; set; }  
}