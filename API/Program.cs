using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Injecting services/dependencies to the container.

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Db Connection (using SQLite as DB)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(connectionString));

// Extension Static method to extend our service injection
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerService();

var app = builder.Build();

// Call this first to apply migrations before any middleware
await app.ApplyMigrationsAsync();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();
