using Microsoft.AspNetCore.Mvc;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;

namespace Supermarket.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ProductDto>>>> GetAll()
    {
        var response = new Response<List<ProductDto>>();
        var products = await _productRepository.GetAllAsync();
        response.Data = products.Select(p => new ProductDto(p)).ToList();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ProductDto>>> GetById(int id)
    {
        var response = new Response<ProductDto>();
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            response.Errors.Add("Product not found");
            return NotFound(response);
        }
        response.Data = new ProductDto(product);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProductDto>>> Post([FromBody] ProductDto productDto)
    {
        var response = new Response<ProductDto>();
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            ProductCategoryId = productDto.ProductCategoryId,
            EmployeeId = productDto.EmployeeId,  // Asignamos el EmployeeId
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };

        product = await _productRepository.SaveAsync(product);
        productDto.Id = product.Id;
        response.Data = productDto;

        return Created($"/api/[controller]/{product.Id}", response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ProductDto>>> Update([FromBody] ProductDto productDto)
    {
        var response = new Response<ProductDto>();
        var product = await _productRepository.GetById(productDto.Id);
        if (product == null)
        {
            response.Errors.Add("Product not found");
            return NotFound(response);
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.ProductCategoryId = productDto.ProductCategoryId;
        product.EmployeeId = productDto.EmployeeId;  // Asignamos el EmployeeId
        product.UpdatedBy = "";
        product.UpdatedDate = DateTime.Now;

        await _productRepository.UpdateAsync(product);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var deleted = await _productRepository.DeleteAsync(id);
        response.Data = deleted;
        return Ok(response);
    }
}
