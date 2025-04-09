using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee> SaveAsync(Employee employee);
    Task<Employee> UpdateAsync(Employee employee);
    Task<List<Employee>> GetAllAsync();
    Task<Employee> GetById(int id);
    Task<bool> DeleteAsync(int id);
    
    Task<Employee> GetByUsernameAsync(string username);

}