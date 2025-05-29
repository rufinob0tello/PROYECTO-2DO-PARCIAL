using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Services;

public class CustomerService : ICustomerService
{
    private readonly string _baseURL = "http://localhost:5143/";
    private readonly string _endpoint = "api/customers";

    public async Task<Response<List<CustomerDto>>> GetAllAsync()
    {
        try
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
        
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
        
            Console.WriteLine($"API Response: {content}"); // Debug
        
            if (!response.IsSuccessStatusCode)
            {
                return new Response<List<CustomerDto>> { Data = new List<CustomerDto>() };
            }
        
            // Configura el deserializador para manejar mayúsculas/minúsculas
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        
            var result = JsonConvert.DeserializeObject<Response<List<CustomerDto>>>(content, settings);
            return result ?? new Response<List<CustomerDto>> { Data = new List<CustomerDto>() };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new Response<List<CustomerDto>> { Data = new List<CustomerDto>() };
        }
    }


    public async Task<Response<CustomerDto>> GetById(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response<CustomerDto>>(json)!;
    }

    public async Task<Response<CustomerDto>> SaveAsync(CustomerDto customerDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var content = new StringContent(JsonConvert.SerializeObject(customerDto), System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response<CustomerDto>>(json)!;
    }

    public async Task<Response<CustomerDto>> UpdateAsync(CustomerDto customerDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var content = new StringContent(JsonConvert.SerializeObject(customerDto), System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response<CustomerDto>>(json)!;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response<bool>>(json)!;
    }
}
