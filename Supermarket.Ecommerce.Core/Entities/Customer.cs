using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Core.Entities;

public class Customer : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public object Name { get; set; }
}