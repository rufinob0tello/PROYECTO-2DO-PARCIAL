using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.WebSite.Services.Interfaces;

public interface IProductCategoriesService
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory> GetByIdAsync(int id);
    Task CreateAsync(ProductCategory category);
    Task UpdateAsync(ProductCategory category);
    Task DeleteAsync(int id);
}