using Microsoft.AspNetCore.Mvc;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;

namespace Supermarket.Ecommerce.Api.Controllers;

    [ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    
    public ProductsController(IProductRepository productRepository)
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

    [HttpPost]
    public async Task<ActionResult<Response<ProductDto>>> Post([FromBody] ProductDto.ProductCreateDto productDto)
    {
        var response = new Response<ProductDto>();
        var product = new Product()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
            BrandId = productDto.BrandId,
            MainImageUrl = productDto.MainImageUrl // Nueva propiedad
        };
    
        product = await _productRepository.SaveAsync(product);
        response.Data = new ProductDto(product);
    
        return Created($"/api/products/{product.Id}", response);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ProductDto>>> GetById(int id)
    {
        var response = new Response<ProductDto>();
        var product = await _productRepository.GetById(id);

        if (product == null)
        {
            response.Errors.Add("Product Not Found");
            return NotFound(response);
        }
        
        response.Data = new ProductDto(product);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<ProductDto>>> Update(int id, [FromBody] ProductDto productDto)
    {
        var response = new Response<ProductDto>();
        var product = await _productRepository.GetById(id);

        if (product == null)
        {
            response.Errors.Add("Product not found");
            return NotFound(response);
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;
        product.BrandId = productDto.BrandId;
        product.MainImageUrl = productDto.MainImageUrl;

        await _productRepository.UpdateAsync(product);
        response.Data = new ProductDto(product);

        return Ok(response);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _productRepository.DeleteAsync(id);
        response.Data = result;
        return Ok(response);
    }
    
    
}