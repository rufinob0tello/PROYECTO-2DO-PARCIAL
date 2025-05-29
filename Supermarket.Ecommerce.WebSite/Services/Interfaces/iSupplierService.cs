using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.WebSite.Services.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<Supplier> GetByIdAsync(int id);
    Task CreateAsync(Supplier supplier);
    Task UpdateAsync(Supplier supplier);
    Task DeleteAsync(int id);
}