using Microsoft.AspNetCore.Authentication.Cookies;
using Supermarket.Ecommerce.WebSite.Services;
using Supermarket.Ecommerce.WebSite.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos y repositorios


// Servicios de aplicación
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();


// Configuración de autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Cookie.HttpOnly = true;
        options.SlidingExpiration = true;
    });

// Antiforgery
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});

// Razor Pages y Controllers
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // <-- NECESARIO para [ApiController]

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" && !context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Auth/Login");
        return;
    }
    await next();
});

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // <-- Importante que esté antes de Authorization
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // <-- Para controladores API

app.Run();;