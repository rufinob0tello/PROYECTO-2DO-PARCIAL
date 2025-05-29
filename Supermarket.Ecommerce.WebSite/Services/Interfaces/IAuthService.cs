using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Http;

namespace Supermarket.Ecommerce.WebSite.Services.Interfaces;

public interface IAuthService
{
    Task<Response<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    
}