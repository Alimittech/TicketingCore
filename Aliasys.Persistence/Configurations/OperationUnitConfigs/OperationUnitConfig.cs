using Aliasys.Domain.Entities.OperationUnitEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.OperationUnitConfigs
{
    public class OperationUnitConfig : IEntityTypeConfiguration<OperationUnit>
    {
        public void Configure(EntityTypeBuilder<OperationUnit> builder)
        {
            builder.ToTable("OperationUnit", "org");
            builder.HasKey(x => x.Id);
            //builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(1);
            builder.Property(x => x.Code).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.IsDeleted).IsRequired().HasColumnOrder(5);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(6);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(new OperationUnit { Id = 1, Code = 1020, Name = "CEO" },
                            new OperationUnit { Id = 2, Code = 1020, Name = "Deputy CEO" },
                            new OperationUnit { Id = 3, Code = 1030, Name = "Financial" },
                            new OperationUnit { Id = 4, Code = 1040, Name = "Human Resources" },
                            new OperationUnit { Id = 5, Code = 1060, Name = "Quality and Systems" },
                            new OperationUnit { Id = 6, Code = 1070, Name = "Internal Commercial" },
                            new OperationUnit { Id = 7, Code = 1080, Name = "Foreign Commercial" },
                            new OperationUnit { Id = 8, Code = 1110, Name = "Sales" },
                            new OperationUnit { Id = 9, Code = 1130, Name = "Marketing" },
                            new OperationUnit { Id = 10, Code = 1140, Name = "After Sales Service" },
                            new OperationUnit { Id = 11, Code = 1160, Name = "Technical" },
                            new OperationUnit { Id = 12, Code = 1170, Name = "Warehouse" },
                            new OperationUnit { Id = 13, Code = 1180, Name = "Sales Engineering" },
                            new OperationUnit { Id = 14, Code = 1190, Name = "Logestics" },
                            new OperationUnit { Id = 15, Code = 1200, Name = "Software" },
                            new OperationUnit { Id = 16, Code = 1090, Name = "Legal" });
        }
    }
}