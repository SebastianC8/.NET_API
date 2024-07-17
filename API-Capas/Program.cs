using API_Capas.Validators;
using Core.Contracts;
using Core.Implementation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository.Contracts;
using Repository.Data;
using Repository.Data.DTO;
using Repository.Implementation;
using Repository.Utilities.Config;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT SET UP
string key = ConfigManager.AppSetting["jwt-secret-key"]!;
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(option =>
{
    var SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256Signature);

    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = SigningKey
    };
});

// CORS Configuration
builder.Services.AddCors(c =>
    c.AddPolicy("Cors", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    })
);

// Dependency inject - Core
builder.Services.AddScoped<IPeopleRepository, PeopleImplRepository>();
builder.Services.AddScoped<IPeopleCore, PeopleImplCore>();
builder.Services.AddScoped<IPostCore, PostImplCore>();
builder.Services.AddScoped<IBeerCore, BeerImplCore>();

// Dependency inject - Repository
builder.Services.AddScoped<IBeerRepository, BeerImplRepository>();

// This config always must be below the dependency inject of the same service. - HTTPClient
builder.Services.AddHttpClient<IPostCore, PostImplCore>(c =>
{
    // Another way to get configurations from appsettings.json from here.
    c.BaseAddress = new Uri(builder.Configuration["PostBaseUrl"]);
});

// Different kind of dependency inject
/*
 * Singleton: Genera una única instancia del objeto en toda la aplicación (Es el mismo objeto para cualquier cliente)
 */
builder.Services.AddKeyedSingleton<IRandomCore, RandomImplCore>("randomSingleton");
/*
 * Scoped: Genera la misma instancia del objeto para un mismo cliente (Si cambia el cliente, cambia el objeto)
 * */
builder.Services.AddKeyedScoped<IRandomCore, RandomImplCore>("randomScoped");

/*
 * Transient: Genera siempre una instancia diferente para el objeto (Incluso desde el mismo cliente )
 */
builder.Services.AddKeyedTransient<IRandomCore, RandomImplCore>("randomTransient");


// ENTITY FRAMEWORK - DBContext
builder.Services.AddDbContext<StoreContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("storeConnection"), b => b.MigrationsAssembly("API-Capas"));
});

// FluentValidator
builder.Services.AddScoped<IValidator<BeerInsertDTO>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDTO>, BeerUpdateValidator>();

CoreServiceProvider.Provider = builder.Services.BuildServiceProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Comando para agregar nugget JWT por consola
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer