using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> SaveAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task<List<Customer>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Customer> GetById(int id);
}