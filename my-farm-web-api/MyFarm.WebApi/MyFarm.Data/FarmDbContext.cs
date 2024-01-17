using Microsoft.EntityFrameworkCore;
using MyFarm.Data.Entities;
using MyFarm.Data.EntityConfigurations;

namespace MyFarm.Data
{
    public class FarmDbContext : DbContext
    {
        public FarmDbContext(DbContextOptions<FarmDbContext> options)
           : base(options)
        {
        }

        public DbSet<AnimalEntity> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new AnimalEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
