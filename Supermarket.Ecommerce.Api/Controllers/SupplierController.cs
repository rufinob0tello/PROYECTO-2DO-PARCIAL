using Microsoft.AspNetCore.Mvc;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;

namespace Supermarket.Ecommerce.Api.Conxtrollers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierController(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<SupplierDto>>>> GetAll()
    {
        var response = new Response<List<SupplierDto>>();
        var suppliers = await _supplierRepository.GetAllAsync();
        response.Data = suppliers.Select(s => new SupplierDto(s)).ToList();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<SupplierDto>>> GetById(int id)
    {
        var response = new Response<SupplierDto>();
        var supplier = await _supplierRepository.GetById(id);
        if (supplier == null)
        {
            response.Errors.Add("Supplier not found");
            return NotFound(response);
        }

        response.Data = new SupplierDto(supplier);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<SupplierDto>>> Post([FromBody] SupplierDto supplierDto)
    {
        var response = new Response<SupplierDto>();

        var supplier = new Supplier
        {
            Name = supplierDto.Name,
            ContactPhone = supplierDto.ContactPhone,
            Email = supplierDto.Email,
            Address = supplierDto.Address,
            EmployeeId = supplierDto.EmployeeId,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };

        var saved = await _supplierRepository.SaveAsync(supplier);
        if (saved == null)
        {
            response.Errors.Add("Invalid EmployeeId");
            return BadRequest(response);
        }

        supplierDto.Id = saved.Id;
        response.Data = supplierDto;
        return Created($"/api/[controller]/{saved.Id}", response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<SupplierDto>>> Update([FromBody] SupplierDto supplierDto)
    {
        var response = new Response<SupplierDto>();
        var supplier = await _supplierRepository.GetById(supplierDto.Id);

        if (supplier == null)
        {
            response.Errors.Add("Supplier not found");
            return NotFound(response);
        }

        supplier.Name = supplierDto.Name;
        supplier.ContactPhone = supplierDto.ContactPhone;
        supplier.Email = supplierDto.Email;
        supplier.Address = supplierDto.Address;
        supplier.EmployeeId = supplierDto.EmployeeId;
        supplier.UpdatedBy = "";
        supplier.UpdatedDate = DateTime.Now;

        await _supplierRepository.UpdateAsync(supplier);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var deleted = await _supplierRepository.DeleteAsync(id);
        response.Data = deleted;
        return Ok(response);
    }
}
