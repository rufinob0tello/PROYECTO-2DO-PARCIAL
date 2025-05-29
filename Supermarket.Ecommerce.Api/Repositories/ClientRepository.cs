using Dapper;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Api.DataAccess.Interfaces; // Asegúrate de usar este namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.Ecommerce.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbContext _dbContext;

        public ClientRepository(IDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Client> SaveAsync(Client client)
        {
            const string sql = @"
            INSERT INTO Client (Username, FirstName, LastName, Phone)
            VALUES (@Username, @FirstName, @LastName, @Phone);
            SELECT LAST_INSERT_ID();";

            client.Id = await _dbContext.Connection.ExecuteScalarAsync<int>(sql, new
            {
                client.Username,
                client.FirstName,
                client.LastName,
                client.Phone
            }, commandTimeout: 30);

            return client;
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            const string sql = @"
            UPDATE Client 
            SET Username = @Username,
                FirstName = @FirstName, 
                LastName = @LastName, 
                Phone = @Phone
            WHERE Id = @Id";

            await _dbContext.Connection.ExecuteAsync(sql, new
            {
                client.Id,
                client.Username,
                client.FirstName,
                client.LastName,
                client.Phone
            }, commandTimeout: 30);

            return client;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Client";
            var clients = await _dbContext.Connection.QueryAsync<Client>(sql, commandTimeout: 30);
            return clients.AsList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Client WHERE Id = @Id";
            var affectedRows = await _dbContext.Connection.ExecuteAsync(sql, new { Id = id }, commandTimeout: 30);
            return affectedRows > 0;
        }

        public async Task<Client> GetById(int id)
        {
            const string sql = "SELECT * FROM Client WHERE Id = @Id";
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id }, commandTimeout: 30);
        }

        public async Task<Client> GetByUsername(string username)
        {
            const string sql = "SELECT * FROM Client WHERE Username = @Username";
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<Client>(sql, new { Username = username }, commandTimeout: 30);
        }
    }
}