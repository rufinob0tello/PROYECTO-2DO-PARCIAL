using Microsoft.AspNetCore.Mvc;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;

namespace Supermarket.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<CustomerDto>>>> GetAll()
    {
        var response = new Response<List<CustomerDto>>();
        var customers = await _customerRepository.GetAllAsync();
        response.Data = customers.Select(c => new CustomerDto(c)).ToList();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<CustomerDto>>> GetById(int id)
    {
        var response = new Response<CustomerDto>();
        var customer = await _customerRepository.GetById(id);

        if (customer == null)
        {
            response.Errors.Add("Customer not found");
            return NotFound(response);
        }

        response.Data = new CustomerDto(customer);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<CustomerDto>>> Post([FromBody] CustomerDto customerDto)
    {
        var response = new Response<CustomerDto>();

        var customer = new Customer
        {
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            Phone = customerDto.Phone,
            Username = customerDto.Username,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };

        customer = await _customerRepository.SaveAsync(customer);
        response.Data = new CustomerDto(customer);

        return Created($"/api/[controller]/{customer.Id}", response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<CustomerDto>>> Update([FromBody] CustomerDto customerDto)
    {
        var response = new Response<CustomerDto>();
        var customer = await _customerRepository.GetById(customerDto.Id);

        if (customer == null)
        {
            response.Errors.Add("Customer not found");
            return NotFound(response);
        }

        customer.FirstName = customerDto.FirstName;
        customer.LastName = customerDto.LastName;
        customer.Phone = customerDto.Phone;
        customer.Username = customerDto.Username;
        customer.UpdatedBy = "";
        customer.UpdatedDate = DateTime.Now;

        await _customerRepository.UpdateAsync(customer);
        response.Data = new CustomerDto(customer);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _customerRepository.DeleteAsync(id);
        response.Data = result;

        return Ok(response);
    }
}
