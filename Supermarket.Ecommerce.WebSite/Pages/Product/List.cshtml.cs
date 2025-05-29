using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Pages.Product;

public class ListModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ILogger<ListModel> _logger;

    public List<ProductDto> Products { get; set; } = new();

    public ListModel(IProductService productService, ILogger<ListModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            // Aquí deberías tener un método DeleteByIdAsync en tu servicio
            var response = await _productService.DeleteByIdAsync(id);
            if (!response.Success)
            {
                _logger.LogWarning("No se pudo eliminar el producto con ID {Id}", id);
            }

            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar producto");
            return RedirectToPage("/Error");
        }
    }


    public async Task<IActionResult> OnGet()
    {
        try
        {
            var response = await _productService.GetAllAsync();
            Products = response.Data ?? new List<ProductDto>();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar productos");
            return RedirectToPage("/Error");
        }
    }
}