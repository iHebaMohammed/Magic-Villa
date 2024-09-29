using Demo.DAL.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.DAL.Data
{
    public class MagicVillaContextSeed
    {
        public static async Task SeedAsync(MagicVillaDbContext context , ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Villas.Any())
                {
                    var villaData = File.ReadAllText("../Demo.DAL/Data/DataSeed/VillaDataSeed.json");
                    var villas = JsonSerializer.Deserialize<List<Villa>>(villaData);
                    foreach (var villa in villas)
                        await context.Villas.AddAsync(villa);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<MagicVillaContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
