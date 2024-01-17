using Microsoft.EntityFrameworkCore;
using MyFarm.Data;
using MyFarm.Data.Entities;
using System.Reflection;

namespace MyFarm.WebApi.Services
{
    /// <summary>
    /// A service that contains methods used to perform application initialization.
    /// </summary>
    public class AppInitializer
    {
        private readonly FarmDbContext _farmDbContext;

        public AppInitializer(FarmDbContext farmDbContext)
        {
            _farmDbContext = farmDbContext;
        }

        public async Task SeedAsync() 
        {
            await _farmDbContext.Database.EnsureCreatedAsync();

            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SampleData", "animals.json");
            var fileContent = await File.ReadAllTextAsync(fileName);
            
            var animals = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AnimalEntity>>(fileContent);
            
            await _farmDbContext.Animals.AddRangeAsync(animals);
            await _farmDbContext.SaveChangesAsync();
        }
    }
}
