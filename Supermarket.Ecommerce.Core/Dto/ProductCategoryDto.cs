namespace Supermarket.Ecommerce.Core.Dto
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; } 

        public ProductCategoryDto() {}
        
        public ProductCategoryDto(Entities.ProductCategory productCategory)
        {
            Id = productCategory.Id;
            Name = productCategory.Name;
            Description = productCategory.Description;
            EmployeeId = productCategory.EmployeeId;
        }
    }
}