using Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext,ILoggerFactory loggerFactory)
        {
            try
            {
#pragma warning disable EF1001 // Internal EF Core API usage.
                if (!storeContext.ProductBrands.Any())
                {
                    var bandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var bands = JsonSerializer.Deserialize<List<ProductBrand>>(bandsData);

                    foreach (var item in bands)
                    {
                        storeContext.ProductBrands.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }
#pragma warning disable EF1001 // Internal EF Core API usage.

                if (!storeContext.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        storeContext.ProductTypes.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }

#pragma warning disable EF1001 // Internal EF Core API usage.
                if (!storeContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        storeContext.Products.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }
#pragma warning restore EF1001 // Internal EF Core API usage.
            }
            catch(Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
