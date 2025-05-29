using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Services;

public class EmployeeService : IEmployeeService
{
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}