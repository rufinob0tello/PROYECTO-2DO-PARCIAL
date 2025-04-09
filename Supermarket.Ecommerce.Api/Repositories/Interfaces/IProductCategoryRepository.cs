using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories.Interfaces;
//ORIGEN DE DATOS
public interface IProductCategoryRepository
{
    //Método para guardar las categorías de un producto
    Task<ProductCategory> SaveAsync(ProductCategory category);
    
    //Método para actualizar las categorías de un producto
    Task<ProductCategory> UpdateAsync(ProductCategory category);
    
    //Método para retornar una lista de categorias de producto
    Task<List<ProductCategory>> GetAllAsync();
    
    //Método para retornar el id de las categorias de producto que se borrará
    Task<bool> DeleteAsync(int id);
    
    //Método para obtener una categoria por el id
    Task<ProductCategory> GetById(int id);
    
    
}