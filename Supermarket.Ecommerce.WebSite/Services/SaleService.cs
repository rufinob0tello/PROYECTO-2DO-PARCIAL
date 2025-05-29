using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Services;

public class SaleService : ISaleService
{
    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Sale> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Sale sale)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Sale sale)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}