using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories.Interfaces;
//ORIGEN DE DATOS
public interface IProductRepository
{
    Task<Product> SaveAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<List<Product>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Product> GetById(int id);
}