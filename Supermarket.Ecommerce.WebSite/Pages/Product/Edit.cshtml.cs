using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Pages.Product;

public class EditModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ILogger<EditModel> _logger;

    [BindProperty]
    public ProductDto Product { get; set; } = new();

    public EditModel(IProductService productService, ILogger<EditModel> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var response = await _productService.GetById(id);
        if (response.Data == null)
            return RedirectToPage("/Error");

        Product = response.Data;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            var response = await _productService.UpdateAsync(Product);
            if (response.Success)
                return RedirectToPage("./List");

            ModelState.AddModelError(string.Empty, "No se pudo actualizar el producto.");
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al editar producto");
            return RedirectToPage("/Error");
        }
    }
}