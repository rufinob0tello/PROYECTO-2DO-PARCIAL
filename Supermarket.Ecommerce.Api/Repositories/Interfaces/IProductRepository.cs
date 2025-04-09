using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories.Interfaces;
//ORIGEN DE DATOS
public interface IProductRepository
{
    //Método para guardar un producto
    Task<Product> SaveAsync(Product product);
    
    //Método para actualizar un producto
    Task<Product> UpdateAsync(Product product);
    
    //Método para retornar una lista de productos
    Task<List<Product>> GetAllAsync();
    
    //Método para eliminar un producto por id
    Task<bool> DeleteAsync(int id);
    
    //Método para obtener un producto por id
    Task<Product> GetById(int id);
}