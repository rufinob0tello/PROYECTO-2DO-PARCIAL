using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Core.Dto;

public class ClientDto : BaseDto
{
    public string Username { get; set; } // Nuevo campo
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }

    public ClientDto() { }

    public ClientDto(Client client)
    {
        Id = client.Id;
        Username = client.Username; // Mapea el nuevo campo
        FirstName = client.FirstName;
        LastName = client.LastName;
        Phone = client.Phone;
    }

    public class ClientCreateDto
    {
        public string Username { get; set; } // Nuevo campo
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
    }
}