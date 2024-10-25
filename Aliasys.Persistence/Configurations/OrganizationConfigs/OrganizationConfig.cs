using Aliasys.Domain.Entities.OrganizationEntities;
using Aliasys.Domain.Entities.RegionEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.OrganizationConfigs
{
    public class OrganizationConfig : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organization", "org");
            builder.HasKey(p => p.Id);
            builder.HasOne<Organization>().WithMany().HasForeignKey(p => p.ParentId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Region>().WithMany().HasForeignKey(p => p.RegionId_FK);
            //builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.Id).HasColumnOrder(0);
            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar(150)").HasColumnOrder(1);
            builder.Property(p => p.DomainName).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(2);
            builder.Property(p => p.ParentId_FK).IsRequired().HasColumnOrder(3);
            builder.Property(p => p.RegionId_FK).IsRequired().HasColumnOrder(4);
            builder.Property(p => p.Address).IsRequired().HasColumnType("nvarchar(500)").HasColumnOrder(5);
            builder.Property(p => p.Phone).IsRequired().HasColumnType("nvarchar(20)").HasColumnOrder(6);
            builder.Property(p => p.CreatedDateTime).HasColumnOrder(7);
            builder.Property(p => p.UpdatedDateTime).HasColumnOrder(8);
            builder.Property(p => p.IsDeleted).HasColumnOrder(9);
            builder.Property(p => p.DeletedDateTime).HasColumnOrder(10);

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasData(new Organization
            {
                Id = 1,
                ParentId_FK = 1,
                Name = "Aliasys Company",
                DomainName = "Aliasys.co",
                RegionId_FK = 1,
                Address = "No. 32, Mohajer Aly., North Sohrevardi St., Seyed Khandan Brg., Tehran, Iran",
                Phone = "00982182455000"
            });
        }
    }
}
