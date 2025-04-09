using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.Core.Dto;

public class EmployeeDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Username { get; set; }

    public EmployeeDto() {}

    public EmployeeDto(Employee employee)
    {
        Id = employee.Id;
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Phone = employee.Phone;
        Username = employee.Username;
    }
}