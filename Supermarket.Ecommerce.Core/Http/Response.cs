using System.Runtime.InteropServices.JavaScript;

namespace Supermarket.Ecommerce.Core.Http;

public class Response<T> //Tipo generico, para que las respuestas sean de cualquier tipo
{
    public T Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    
    public Response(T data = default)
    {
        Data = data;
    }
    
}