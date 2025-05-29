using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Api.Repositories.Interfaces;

public interface ISaleRepository
{
    Task<bool> CreateSaleAsync(SaleRequestDto saleRequest, decimal total);
}
