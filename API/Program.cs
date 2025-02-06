using API.Extensions;
using API.Middleware;
using API.SignalR;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------- Dependencies Injected --------------------------------------------------- //

// Injecting services/dependencies to the container.
builder.Services.AddControllers();

// SQL Server Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlServer(connectionString));

// Redis Database
builder.Services.AddSingleton<IConnectionMultiplexer>(config => 
{
    var connString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Cannot get Redis connection string");
    var configuration = ConfigurationOptions.Parse(connString, true);

    return ConnectionMultiplexer.Connect(configuration);
});

// Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Identity
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<StoreContext>();

// Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Cart
builder.Services.AddSingleton<ICartService, CartService>();

// Payments
builder.Services.AddScoped<IPaymentService, PaymentService>();

// SignalR
builder.Services.AddSignalR();

// Swagger
builder.Services.AddSwaggerService();

// CORS
builder.Services.AddCors();

// Building the App
var app = builder.Build();

// Call this first to apply migrations before any middleware
await app.ApplyMigrationsAsync();

// --------------------------------------- Middlewares --------------------------------------------------- //
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

// CORS needs to be here ! We are allowing our client here !
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSwaggerDocumentation();

app.MapControllers();

app.MapGroup("api").MapIdentityApi<AppUser>();

app.MapHub<NotificationHub>("/hub/notifications");

app.Run();
