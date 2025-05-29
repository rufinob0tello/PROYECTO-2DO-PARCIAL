using Supermarket.Ecommerce.Core.Entities;

public interface IClientRepository
{
    Task<Client> SaveAsync(Client client);
    Task<Client> UpdateAsync(Client client);
    Task<List<Client>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Client> GetById(int id);
    Task<Client> GetByUsername(string username); // Cambiado de AuthUserId a Username
}