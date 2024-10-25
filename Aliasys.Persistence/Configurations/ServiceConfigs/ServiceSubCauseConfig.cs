using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceSubCauseConfig : IEntityTypeConfiguration<ServiceSubCause>
    {
        public void Configure(EntityTypeBuilder<ServiceSubCause> builder)
        {
            builder.ToTable("ServiceSubCause", "srv");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.SubCauseName ).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.SubCauseName).IsRequired().HasColumnType("nvarchar(50)").HasColumnOrder(1);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(2);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(3);
            builder.Property(x => x.IsDeleted).HasColumnOrder(4);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(5);

            builder.HasData(new ServiceSubCause { Id = 1, SubCauseName = "SSL Issue" },
                            new ServiceSubCause { Id = 2, SubCauseName = "License" },
                            new ServiceSubCause { Id = 3, SubCauseName = "Database Performance" },
                            new ServiceSubCause { Id = 4, SubCauseName = "User Mistake" },
                            new ServiceSubCause { Id = 5, SubCauseName = "Wrong Configuration" },
                            new ServiceSubCause { Id = 6, SubCauseName = "Report Development" },
                            new ServiceSubCause { Id = 7, SubCauseName = "Process Development" },
                            new ServiceSubCause { Id = 8, SubCauseName = "User Access" },
                            new ServiceSubCause { Id = 9, SubCauseName = "Form Adjustment" },
                            new ServiceSubCause { Id = 10, SubCauseName = "Wrong Master Data" });
        }
    }
}
