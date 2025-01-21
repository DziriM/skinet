using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        var logger = (ILogger)loggerFactory.CreateLogger<StoreContextSeed>();

        try
        {
            #region SeedProductBrands

            if (!context.ProductBrands.Any())
            {
                var brandsData = await File.ReadAllTextAsync(@"../Infrastructure/Data/SeedData/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands == null) throw new NullReferenceException("no brands available for seeding");

                foreach (var item in brands)
                {
                    context.ProductBrands.Add(item);
                }

                logger.LogInformation("Seeding ProductBrands...");
                await context.SaveChangesAsync();
                logger.LogInformation("Seeding ProductBrands Complete");
            }

            #endregion

            #region SeedProductTypes

            if (!context.ProductTypes.Any())
            {
                var typesData = await File.ReadAllTextAsync(@"../Infrastructure/Data/SeedData/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types == null) throw new NullReferenceException("no types available for seeding");

                foreach (var item in types)
                {
                    context.ProductTypes.Add(item);
                }

                logger.LogInformation("Seeding ProductTypes...");
                await context.SaveChangesAsync();
                logger.LogInformation("Seeding ProductTypes Complete");
            }

            #endregion
            
            #region SeedProducts

            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"../Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products == null) throw new NullReferenceException("no products available for seeding");

                foreach (var item in products)
                {
                    context.Products.Add(item);
                }

                logger.LogInformation("Seeding Products...");
                await context.SaveChangesAsync();
                logger.LogInformation("Seeding Products Complete");
            }

            #endregion
        }
        catch (Exception ex)
        {
            logger = loggerFactory.CreateLogger<StoreContextSeed>();

            logger.LogError(ex, "An error occurred during migration");
        }
    }
}