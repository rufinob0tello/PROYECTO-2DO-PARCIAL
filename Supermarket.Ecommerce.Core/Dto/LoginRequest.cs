namespace Supermarket.Ecommerce.Core.Dto;

public class LoginRequestDto
{
    public string Username { get; set; }
    public string Phone { get; set; }
}

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public ClientDto Client { get; set; }
}