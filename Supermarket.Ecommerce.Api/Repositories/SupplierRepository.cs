using Dapper;
using Dapper.Contrib.Extensions;
using Supermarket.Ecommerce.Api.DataAccess.Interfaces;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;
using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly IDbContext _dbContext;

    public SupplierRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Supplier> SaveAsync(Supplier supplier)
    {
        // Verificar si el empleado existe
        var employee = await _dbContext.Connection.GetAsync<Employee>(supplier.EmployeeId);
        if (employee == null) return null;

        supplier.Id = await _dbContext.Connection.InsertAsync(supplier);
        return supplier;
    }

    public async Task<Supplier> UpdateAsync(Supplier supplier)
    {
        await _dbContext.Connection.UpdateAsync(supplier);
        return supplier;
    }

    public async Task<List<Supplier>> GetAllAsync()
    {
        var sql = "SELECT * FROM Supplier WHERE IsDeleted = 0";
        var suppliers = await _dbContext.Connection.QueryAsync<Supplier>(sql);
        return suppliers.ToList();
    }

    public async Task<Supplier> GetById(int id)
    {
        var supplier = await _dbContext.Connection.GetAsync<Supplier>(id);
        return supplier?.IsDeleted == true ? null : supplier;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var supplier = await GetById(id);
        if (supplier == null) return false;

        supplier.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(supplier);
    }
}