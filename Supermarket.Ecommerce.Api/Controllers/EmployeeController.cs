using Microsoft.AspNetCore.Mvc;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;

namespace Supermarket.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<EmployeeDto>>>> GetAll()
    {
        var response = new Response<List<EmployeeDto>>();
        var employees = await _employeeRepository.GetAllAsync();
        response.Data = employees.Select(e => new EmployeeDto(e)).ToList();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<EmployeeDto>>> GetById(int id)
    {
        var response = new Response<EmployeeDto>();
        var employee = await _employeeRepository.GetById(id);
        if (employee == null)
        {
            response.Errors.Add("Employee not found");
            return NotFound(response);
        }

        response.Data = new EmployeeDto(employee);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<EmployeeDto>>> Post([FromBody] EmployeeDto dto)
    {
        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Phone = dto.Phone,
            Username = dto.Username,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };

        employee = await _employeeRepository.SaveAsync(employee);
        return Created($"/api/[controller]/{employee.Id}", new Response<EmployeeDto>(new EmployeeDto(employee)));
    }

    [HttpPut]
    public async Task<ActionResult<Response<EmployeeDto>>> Update([FromBody] EmployeeDto dto)
    {
        var employee = await _employeeRepository.GetById(dto.Id);
        if (employee == null)
            return NotFound(new Response<EmployeeDto> { Errors = { "Employee not found" } });

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Phone = dto.Phone;
        employee.Username = dto.Username;
        employee.UpdatedBy = "";
        employee.UpdatedDate = DateTime.Now;

        await _employeeRepository.UpdateAsync(employee);
        return Ok(new Response<EmployeeDto>(new EmployeeDto(employee)));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var deleted = await _employeeRepository.DeleteAsync(id);
        var response = new Response<bool> { Data = deleted };
        return Ok(response);
    }
    
    [HttpGet("username/{username}")]
    public async Task<ActionResult<Response<EmployeeDto>>> GetByUsername(string username)
    {
        var response = new Response<EmployeeDto>();
        var employee = await _employeeRepository.GetByUsernameAsync(username);

        if (employee == null)
        {
            response.Errors.Add("Empleado no encontrado por username.");
            return NotFound(response);
        }

        response.Data = new EmployeeDto(employee);
        return Ok(response);
    }


}
