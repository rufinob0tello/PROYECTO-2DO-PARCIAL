using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Core.Dto;

public class SupplierDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactPhone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int EmployeeId { get; set; }

    public SupplierDto() {}

    public SupplierDto(Supplier supplier)
    {
        Id = supplier.Id;
        Name = supplier.Name;
        ContactPhone = supplier.ContactPhone;
        Email = supplier.Email;
        Address = supplier.Address;
        EmployeeId = supplier.EmployeeId;
    }
}