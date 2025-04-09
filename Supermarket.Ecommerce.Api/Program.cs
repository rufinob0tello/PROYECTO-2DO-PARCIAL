using Dapper;
using Dapper.Contrib.Extensions;
using Supermarket.Ecommerce.Api.DataAccess;
using Supermarket.Ecommerce.Api.DataAccess.Interfaces;
using Supermarket.Ecommerce.Api.Repositories;
using Supermarket.Ecommerce.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IProductCategoryRepository, InMemoryProductCategoryRepository>();
builder.Services.AddSingleton<IProductCategoryRepository, ProductCategoryRepository>(); //cuando se hace una nueva solicitud se guarda en memoria
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddSingleton<ISupplierRepository, SupplierRepository>();
builder.Services.AddSingleton<IDbContext, DbContext>();

SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.ToString();
    if (name.Contains("Supermarket.Ecommerce.Core.Entities."))
        name = name.Replace("Supermarket.Ecommerce.Core.Entities.", "");
    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string (letters);
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();