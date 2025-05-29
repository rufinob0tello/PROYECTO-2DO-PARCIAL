using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using MySqlConnector;
using Supermarket.Ecommerce.Api.DataAccess.Interfaces;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly IDbConnection _connection;

    public SaleRepository(IConfiguration configuration)
    {
        _connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<bool> CreateSaleAsync(SaleRequestDto saleRequest, decimal total)
    {
        using var transaction = _connection.BeginTransaction();
        try
        {
            var sale = new Sale
            {
                CustomerId = saleRequest.CustomerId,
                Total = total,
                CreatedBy = "",
                CreatedDate = DateTime.Now
            };

            var saleId = await _connection.InsertAsync(sale, transaction);

            foreach (var item in saleRequest.Products)
            {
                var saleDetail = new SaleDetail
                {
                    SaleId = (int)saleId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CreatedBy = "",
                    CreatedDate = DateTime.Now
                };
                await _connection.InsertAsync(saleDetail, transaction);
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }
}
