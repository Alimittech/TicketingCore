using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.ServiceConfigs
{
    public class ServiceCauseCategoryConfig : IEntityTypeConfiguration<ServiceCauseCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCauseCategory> builder)
        {
            builder.ToTable("ServiceCauseCategory", "srv");
            builder.HasKey(x => x.Id);
            builder.HasOne<ServiceCategory>().WithMany().HasForeignKey(x => x.ServiceCategoryId_FK);
            builder.HasOne<ServiceRootCause>().WithMany().HasForeignKey(x => x.ServiceRootCauseId_FK);
            builder.HasOne<ServiceSubCause>().WithMany().HasForeignKey(x => x.ServiceSubCauseId_FK);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.ServiceCategoryId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.ServiceRootCauseId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.ServiceSubCauseId_FK).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(4);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(5);
            builder.Property(x => x.IsDeleted).HasColumnOrder(6);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(7);

            builder.HasData(new ServiceCauseCategory { Id = 1, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 1, ServiceSubCauseId_FK = 1 },
                            new ServiceCauseCategory { Id = 2, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 2, ServiceSubCauseId_FK = 2 },
                            new ServiceCauseCategory { Id = 3, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 2, ServiceSubCauseId_FK = 3 },
                            new ServiceCauseCategory { Id = 4, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 2, ServiceSubCauseId_FK = 4 },
                            new ServiceCauseCategory { Id = 5, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 2, ServiceSubCauseId_FK = 5 },
                            new ServiceCauseCategory { Id = 6, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 3, ServiceSubCauseId_FK = 6 },
                            new ServiceCauseCategory { Id = 7, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 3, ServiceSubCauseId_FK = 7 },
                            new ServiceCauseCategory { Id = 8, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 3, ServiceSubCauseId_FK = 8 },
                            new ServiceCauseCategory { Id = 9, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 3, ServiceSubCauseId_FK = 9 },
                            new ServiceCauseCategory { Id = 10, ServiceCategoryId_FK = 1, ServiceRootCauseId_FK = 3, ServiceSubCauseId_FK = 10 }
                            );
        }
    }
}
