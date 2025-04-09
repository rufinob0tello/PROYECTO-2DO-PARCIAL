using Dapper;
using Dapper.Contrib.Extensions;
using Supermarket.Ecommerce.Api.DataAccess.Interfaces;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;
using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbContext _dbContext;

    public CustomerRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Customer> SaveAsync(Customer customer)
    {
        customer.Id = await _dbContext.Connection.InsertAsync(customer);
        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        await _dbContext.Connection.UpdateAsync(customer);
        return customer;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Customer WHERE IsDeleted = 0";
        var customers = await _dbContext.Connection.QueryAsync<Customer>(sql);
        return customers.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await GetById(id);
        if (customer == null)
            return false;

        customer.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(customer);
    }

    public async Task<Customer> GetById(int id)
    {
        var customer = await _dbContext.Connection.GetAsync<Customer>(id);
        return customer == null || customer.IsDeleted ? null : customer;
    }
}