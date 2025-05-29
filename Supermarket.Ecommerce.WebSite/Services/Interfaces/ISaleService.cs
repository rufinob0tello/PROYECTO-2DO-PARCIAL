using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.WebSite.Services.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale> GetByIdAsync(int id);
    Task CreateAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(int id);
}