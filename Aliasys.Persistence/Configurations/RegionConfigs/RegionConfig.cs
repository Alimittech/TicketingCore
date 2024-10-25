using Aliasys.Domain.Entities.RegionEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.RegionConfigs
{
    public class RegionConfig : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Region", "rgn");
            builder.HasKey(p => p.Id);
            //builder.HasAlternateKey(p => new { p.CountryName, p.CapitalName }).HasName("IX_RegionMultiColumns");

            builder.Property(p => p.Id).IsRequired().HasColumnOrder(0);
            builder.Property(p => p.CountryName).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(1);
            builder.Property(p => p.CapitalName).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(2);
            builder.Property(p => p.ContinentName).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(3);
            builder.Property(p => p.Flag).HasColumnType("nvarchar(500)").HasColumnOrder(4);

            builder.HasData(
                new Region { Id = 1, CountryName = "Iran", CapitalName = "Tehran", ContinentName = Continent.Asia },
                new Region { Id = 2, CountryName = "United Arab Emirates", CapitalName = "Dubai", ContinentName = Continent.Asia });
        }
    }
}
