using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories.Interfaces;

public interface ISupplierRepository
{
    Task<Supplier> SaveAsync(Supplier supplier);
    Task<Supplier> UpdateAsync(Supplier supplier);
    Task<List<Supplier>> GetAllAsync();
    Task<Supplier> GetById(int id);
    Task<bool> DeleteAsync(int id);
}