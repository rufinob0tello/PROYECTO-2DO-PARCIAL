using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Pages.Product;

public class CreateModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ILogger<CreateModel> _logger;

    [BindProperty]
    public ProductDto Product { get; set; } = new();

    public CreateModel(IProductService productService, ILogger<CreateModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public void OnGet()
    {
        Product.CategoryId = 1;
        Product.BrandId = 1;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            var response = await _productService.CreateAsync(Product);
            if (response.Success)
                return RedirectToPage("./List");

            ModelState.AddModelError(string.Empty, "No se pudo crear el producto.");
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear producto");
            return RedirectToPage("/Error");
        }
    }
}