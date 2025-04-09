using Microsoft.AspNetCore.Mvc;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;

namespace Supermarket.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ProductCategoryDto>>>> GetAll()
    {
        var response = new Response<List<ProductCategoryDto>>();
        var categories = await _productCategoryRepository.GetAllAsync();
        response.Data = categories.Select(c => new ProductCategoryDto(c)).ToList();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ProductCategoryDto>>> GetById(int id)
    {
        var response = new Response<ProductCategoryDto>();
        var category = await _productCategoryRepository.GetById(id);
        if (category == null)
        {
            response.Errors.Add("Product Category not found");
            return NotFound(response);
        }
        response.Data = new ProductCategoryDto(category);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProductCategoryDto>>> Post([FromBody] ProductCategoryDto categoryDto)
    {
        var response = new Response<ProductCategoryDto>();
        var category = new ProductCategory
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description,
            EmployeeId = categoryDto.EmployeeId,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };

        category = await _productCategoryRepository.SaveAsync(category);
        categoryDto.Id = category.Id;
        response.Data = categoryDto;

        return Created($"/api/[controller]/{category.Id}", response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ProductCategoryDto>>> Update([FromBody] ProductCategoryDto categoryDto)
    {
        var response = new Response<ProductCategoryDto>();
        var category = await _productCategoryRepository.GetById(categoryDto.Id);
        if (category == null)
        {
            response.Errors.Add("ProductCategory not found");
            return NotFound(response);
        }

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;
        category.EmployeeId = categoryDto.EmployeeId; // Asignamos el EmployeeId
        category.UpdatedBy = "";
        category.UpdatedDate = DateTime.Now;

        await _productCategoryRepository.UpdateAsync(category);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var deleted = await _productCategoryRepository.DeleteAsync(id);
        response.Data = deleted;
        return Ok(response);
    }
}
