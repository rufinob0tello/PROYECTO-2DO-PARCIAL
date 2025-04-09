using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Core.Dto;

public class CustomerDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Username { get; set; }

    public CustomerDto() { }

    public CustomerDto(Customer customer)
    {
        Id = customer.Id;
        FirstName = customer.FirstName;
        LastName = customer.LastName;
        Phone = customer.Phone;
        Username = customer.Username;
    }
}