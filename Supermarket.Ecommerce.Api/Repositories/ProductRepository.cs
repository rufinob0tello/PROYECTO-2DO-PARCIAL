using Dapper;
using Dapper.Contrib.Extensions;
using Supermarket.Ecommerce.Api.DataAccess.Interfaces;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;
using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbContext _dbContext;

    public ProductRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Product> SaveAsync(Product product)
    {
        product.Id = await _dbContext.Connection.InsertAsync(product);
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        await _dbContext.Connection.UpdateAsync(product);
        return product;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Product WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Product>(sql);
        return result.ToList();
    }

    public async Task<Product> GetById(int id)
    {
        var product = await _dbContext.Connection.GetAsync<Product>(id);
        return product == null || product.IsDeleted ? null : product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await GetById(id);
        if (product == null) return false;

        product.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(product);
    }
}