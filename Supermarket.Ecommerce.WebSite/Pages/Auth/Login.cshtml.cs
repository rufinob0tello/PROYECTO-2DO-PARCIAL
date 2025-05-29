using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;
using Supermarket.Ecommerce.Core.Dto;

namespace Supermarket.Ecommerce.WebSite.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly IAuthService _authService;

    [BindProperty]
    public LoginRequestDto LoginRequest { get; set; }

    public LoginModel(IAuthService authService)
    {
        _authService = authService;
    }

    public void OnGet()
    {
        // Si ya está autenticado, redirigir
        if (User.Identity.IsAuthenticated)
        {
            Response.Redirect("/Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Cierra sesión anterior (por si acaso)
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var response = await _authService.LoginAsync(LoginRequest);

        if (response.Data != null && response.Data.Success)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Data.Client.Id.ToString()),
                new Claim(ClaimTypes.Name, response.Data.Client.Username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            });

            return RedirectToPage("/Product/List"); // o donde quieras llevarlo
        }

        ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
        return Page();
    }
}
