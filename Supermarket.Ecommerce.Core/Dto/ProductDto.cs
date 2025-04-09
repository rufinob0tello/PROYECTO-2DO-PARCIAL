using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Core.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int ProductCategoryId { get; set; }
    public int EmployeeId { get; set; }

    public ProductDto() { }

    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        ProductCategoryId = product.ProductCategoryId;
        EmployeeId = product.EmployeeId;
    }
}