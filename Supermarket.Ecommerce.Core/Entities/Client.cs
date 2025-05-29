namespace Supermarket.Ecommerce.Core.Entities;

public class Client : EntityBase
{
    public string Username { get; set; } = string.Empty; // Reemplaza AuthUserId
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
}