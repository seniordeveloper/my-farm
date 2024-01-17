using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFarm.Data.Entities;

namespace MyFarm.Data.EntityConfigurations
{
    class AnimalEntityTypeConfiguration : IEntityTypeConfiguration<AnimalEntity>
    {
        public void Configure(EntityTypeBuilder<AnimalEntity> builder) 
        {
            builder.ToTable(nameof(FarmDbContext.Animals));

            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Name)
                   .HasMaxLength(70)
                   .IsUnicode();

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }

    }
}
