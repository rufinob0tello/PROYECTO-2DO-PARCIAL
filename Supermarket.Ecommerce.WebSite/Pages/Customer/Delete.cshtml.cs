using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Pages.Customer;

public class Delete : PageModel
{
    [BindProperty] public CustomerDto Customer { get; set; }

    private readonly ICustomerService _service;

    public Delete(ICustomerService service)
    {
        _service = service;
        Customer = new CustomerDto();
    }

    public async Task<IActionResult> OnGet(int id)
    {
        var response = await _service.GetById(id);
        Customer = response.Data;

        if (Customer == null) return RedirectToPage("/Error");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _service.DeleteAsync(Customer.Id);
        return RedirectToPage("./List");
    }
}