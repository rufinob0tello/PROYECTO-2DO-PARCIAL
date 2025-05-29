using Newtonsoft.Json;

namespace Supermarket.Ecommerce.Core.Http;

public class Response<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();
    
    public bool IsSuccess { get; set; }
    public bool Success { get; set; }
}