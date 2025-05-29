using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Http;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

namespace Supermarket.Ecommerce.WebSite.Pages.Customer;

public class Edit : PageModel
{
    [BindProperty] public CustomerDto Customer { get; set; }

    private readonly ICustomerService _service;

    public Edit(ICustomerService service)
    {
        _service = service;
        Customer = new CustomerDto();
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id.HasValue)
        {
            var response = await _service.GetById(id.Value);
            if (response.Data == null) return RedirectToPage("/Error");
            Customer = response.Data;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        Response<CustomerDto> response;

        if (Customer.Id > 0)
            response = await _service.UpdateAsync(Customer);
        else
            response = await _service.SaveAsync(Customer);

        return RedirectToPage("./List");
    }
}