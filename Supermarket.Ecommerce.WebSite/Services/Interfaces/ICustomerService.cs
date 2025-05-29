using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Http;

namespace Supermarket.Ecommerce.WebSite.Services.Interfaces;

public interface ICustomerService
{
    Task<Response<List<CustomerDto>>> GetAllAsync();
    Task<Response<CustomerDto>> GetById(int id);
    Task<Response<CustomerDto>> SaveAsync(CustomerDto customerDto);
    Task<Response<CustomerDto>> UpdateAsync(CustomerDto customerDto);
    Task<Response<bool>> DeleteAsync(int id);
}