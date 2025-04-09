using Dapper;
using Dapper.Contrib.Extensions;
using Supermarket.Ecommerce.Api.DataAccess.Interfaces;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;
using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContext _dbContext;

    public EmployeeRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Employee> SaveAsync(Employee employee)
    {
        employee.Id = await _dbContext.Connection.InsertAsync(employee);
        return employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        await _dbContext.Connection.UpdateAsync(employee);
        return employee;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Employee WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Employee>(sql);
        return result.ToList();
    }

    public async Task<Employee> GetById(int id)
    {
        var employee = await _dbContext.Connection.GetAsync<Employee>(id);
        return employee == null || employee.IsDeleted ? null : employee;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await GetById(id);
        if (employee == null) return false;

        employee.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(employee);
    }
    
    public async Task<Employee> GetByUsernameAsync(string username)
    {
        const string sql = "SELECT * FROM Employee WHERE Username = @Username AND IsDeleted = 0";
        var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Username = username });
        return result;
    }

}