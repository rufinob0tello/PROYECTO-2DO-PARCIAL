using Newtonsoft.Json;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Services;

public class AuthService : IAuthService
{
    private readonly string _baseURL = "http://localhost:5143/";
    private readonly string _endpoint = "api/client";

    public async Task<Response<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var url = $"{_baseURL}{_endpoint}/byusername/{request.Username}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<ClientDto>>(json);

        if (response.Data == null || response.Data.Phone != request.Phone)
        {
            return new Response<LoginResponseDto>
            {
                Data = new LoginResponseDto
                {
                    Success = false,
                    Message = "Credenciales inválidas"
                }
            };
        }

        return new Response<LoginResponseDto>
        {
            Data = new LoginResponseDto
            {
                Success = true,
                Message = "Inicio sesión",
                Client = response.Data
            }
        };
    }
}