using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Supermarket.Ecommerce.WebSite.Pages.Customer;

public class ListModel : PageModel
{
    private readonly ICustomerService _service;

    public List<CustomerDto> Customers { get; set; }

    public ListModel(ICustomerService service)
    {
        _service = service;
        Customers = new List<CustomerDto>();
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Console.WriteLine($"Customers received: {JsonConvert.SerializeObject(response)}");
    
        Customers = response?.Data ?? new List<CustomerDto>();
        return Page();
    }

}