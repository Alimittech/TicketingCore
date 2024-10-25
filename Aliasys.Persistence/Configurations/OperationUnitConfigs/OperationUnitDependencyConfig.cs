using Aliasys.Domain.Entities.OperationUnitEntities;
using Aliasys.Domain.Entities.OrganizationEntities;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.OperationUnitConfigs
{
    public class OperationUnitDependencyConfig : IEntityTypeConfiguration<OperationUnitDependency>
    {
        public void Configure(EntityTypeBuilder<OperationUnitDependency> builder)
        {
            builder.ToTable("OperationUnitDependency", "org");
            builder.HasKey(x => x.Id);
            builder.HasOne<Organization>().WithMany().HasForeignKey(x => x.OrganizationId_FK);
            builder.HasOne<OperationUnit>().WithMany().HasForeignKey(x =>x.OperationUnitId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<OperationUnit>().WithMany().HasForeignKey(x =>x.ParentOperationUnitId_FK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<User>().WithMany().HasForeignKey(x => x.ManagerId_FK);
            //builder.HasIndex(x => new {x.OrganizationId_FK, x.OperationUnitId_FK, x.ParentOperationUnitId_FK,x.ManagerId_FK}).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.OrganizationId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.OperationUnitId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.ParentOperationUnitId_FK).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.ManagerId_FK).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(5);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(6);
            builder.Property(x => x.IsDeleted).HasColumnOrder(7);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(8);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(new OperationUnitDependency { Id = 1, OrganizationId_FK = 1, OperationUnitId_FK = 1, ParentOperationUnitId_FK = 1, ManagerId_FK = 1 },//CEO
                            new OperationUnitDependency { Id = 2, OrganizationId_FK = 1, OperationUnitId_FK = 2, ParentOperationUnitId_FK = 1, ManagerId_FK = 2 },//Deputy CEO
                            new OperationUnitDependency { Id = 3, OrganizationId_FK = 1, OperationUnitId_FK = 3, ParentOperationUnitId_FK = 2, ManagerId_FK = 14 },//Financial
                            new OperationUnitDependency { Id = 4, OrganizationId_FK = 1, OperationUnitId_FK = 4, ParentOperationUnitId_FK = 2, ManagerId_FK = 9 },//Human Resources
                            new OperationUnitDependency { Id = 5, OrganizationId_FK = 1, OperationUnitId_FK = 5, ParentOperationUnitId_FK = 2, ManagerId_FK = 10 },//Quality and systems
                            new OperationUnitDependency { Id = 6, OrganizationId_FK = 1, OperationUnitId_FK = 6, ParentOperationUnitId_FK = 2, ManagerId_FK = 21 },//Internal Commercial
                            new OperationUnitDependency { Id = 7, OrganizationId_FK = 1, OperationUnitId_FK = 7, ParentOperationUnitId_FK = 2, ManagerId_FK = 8 },//Foreign Commercial
                            new OperationUnitDependency { Id = 8, OrganizationId_FK = 1, OperationUnitId_FK = 8, ParentOperationUnitId_FK = 2, ManagerId_FK = 7 },//Sales
                            new OperationUnitDependency { Id = 9, OrganizationId_FK = 1, OperationUnitId_FK = 9, ParentOperationUnitId_FK = 2, ManagerId_FK = 11 },//Marketing
                            new OperationUnitDependency { Id = 10, OrganizationId_FK = 1, OperationUnitId_FK = 10, ParentOperationUnitId_FK = 2, ManagerId_FK = 16 },//After Sales Service
                            new OperationUnitDependency { Id = 11, OrganizationId_FK = 1, OperationUnitId_FK = 11, ParentOperationUnitId_FK = 2, ManagerId_FK = 6 },//Technical
                            new OperationUnitDependency { Id = 12, OrganizationId_FK = 1, OperationUnitId_FK = 12, ParentOperationUnitId_FK = 2, ManagerId_FK = 12 },//Warehouse
                            new OperationUnitDependency { Id = 13, OrganizationId_FK = 1, OperationUnitId_FK = 13, ParentOperationUnitId_FK = 2, ManagerId_FK = 5 },//Sales engineering
                            new OperationUnitDependency { Id = 14, OrganizationId_FK = 1, OperationUnitId_FK = 14, ParentOperationUnitId_FK = 2, ManagerId_FK = 13 },//Logestics
                            new OperationUnitDependency { Id = 15, OrganizationId_FK = 1, OperationUnitId_FK = 15, ParentOperationUnitId_FK = 2, ManagerId_FK = 4 });//Software
        }
    }
}
