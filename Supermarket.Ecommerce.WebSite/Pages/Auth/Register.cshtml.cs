using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.Core.Dto;
using Supermarket.Ecommerce.Core.Entities;

namespace Supermarket.Ecommerce.WebSite.Pages.Auth;

public class RegisterModel : PageModel
{
    private readonly IClientRepository _clientRepository;

    [BindProperty]
    public ClientDto.ClientCreateDto Client { get; set; }

    public RegisterModel(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var newClient = new Client
        {
            Username = Client.Username,
            FirstName = Client.FirstName, 
            LastName = Client.LastName, 
            Phone = Client.Phone
        };

        await _clientRepository.SaveAsync(newClient);

        return RedirectToPage("/Auth/Login");
    }
}