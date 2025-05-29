using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;

namespace Supermarket.Ecommerce.WebSite.Services.Interfaces;

public interface IProductService
{
    Task<Response<List<ProductDto>>> GetAllAsync();
    Task<Response<ProductDto>> GetById(int id);
    Task<Response<bool>> DeleteByIdAsync(int id);
    Task<Response<bool>> CreateAsync(ProductDto product);
    Task<Response<bool>> UpdateAsync(ProductDto product);


}