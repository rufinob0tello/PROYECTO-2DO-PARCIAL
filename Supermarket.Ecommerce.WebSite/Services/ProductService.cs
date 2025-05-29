using Newtonsoft.Json;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Services;

public class ProductService : IProductService
{
    private readonly string _baseURL = "http://localhost:5143/";
    private readonly string _endpoint = "api/products";

    public async Task<Response<List<ProductDto>>> GetAllAsync()
    {
        var url = $"{_baseURL}{_endpoint}"; //url completa 
    
        var client = new HttpClient(); //hacer solicitudes
        var res = await client.GetAsync(url); //solicitud http
        var json = await res.Content.ReadAsStringAsync(); //para hacer que venga en un json

        var response = JsonConvert.DeserializeObject<Response<List<ProductDto>>>(json);
    
        return response;
    }

    public async Task<Response<ProductDto>> GetById(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response<ProductDto>>(json);
    }
    
    public async Task<Response<bool>> DeleteByIdAsync(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response<bool>>(json);
    }
    
    public async Task<Response<bool>> CreateAsync(ProductDto product)
    {
        var url = $"{_baseURL}{_endpoint}";
        var client = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(product), System.Text.Encoding.UTF8, "application/json");
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        // Leer la respuesta como Response<ProductDto>
        var intermediateResponse = JsonConvert.DeserializeObject<Response<ProductDto>>(json);

        // Convertirlo en un Response<bool> para que tu app web lo entienda igual
        return new Response<bool>
        {
            Data = intermediateResponse?.Data != null,
            Message = intermediateResponse?.Message,
            Success = intermediateResponse?.Success ?? false,
            IsSuccess = intermediateResponse?.IsSuccess ?? false,
            Errors = intermediateResponse?.Errors ?? new List<string>()
        };
    }


    public async Task<Response<bool>> UpdateAsync(ProductDto product)
    {
        var url = $"{_baseURL}{_endpoint}/{product.Id}";
        var client = new HttpClient();
        var content = new StringContent(JsonConvert.SerializeObject(product), System.Text.Encoding.UTF8, "application/json");
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();

        // El backend devuelve Response<ProductDto>, así que lo leemos primero así
        var intermediateResponse = JsonConvert.DeserializeObject<Response<ProductDto>>(json);

        // Y convertimos a Response<bool>
        return new Response<bool>
        {
            Data = intermediateResponse?.Data != null,
            Message = intermediateResponse?.Message,
            Success = intermediateResponse?.Success ?? false,
            IsSuccess = intermediateResponse?.IsSuccess ?? false,
            Errors = intermediateResponse?.Errors ?? new List<string>()
        };
    }



}