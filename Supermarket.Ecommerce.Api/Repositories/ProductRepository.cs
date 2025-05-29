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
        const string sql = "SELECT * FROM Product";
        var products = await _dbContext.Connection.QueryAsync<Product>(sql);
        return products.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Product WHERE Id = @Id";
        var affectedRows = await _dbContext.Connection.ExecuteAsync(sql, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<Product> GetById(int id)
    {
        const string sql = "SELECT * FROM Product WHERE Id = @Id";
        return await _dbContext.Connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
    }
}