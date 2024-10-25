using Aliasys.Domain.Entities.OperationUnitEntities;
using Aliasys.Domain.Entities.OrganizationEntities;
using Aliasys.Domain.Entities.PositionEntitis;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.UserConfigs
{
    public class UserInOrgOpunitPosConfig : IEntityTypeConfiguration<UserInOrgOpunitPos>
    {
        public void Configure(EntityTypeBuilder<UserInOrgOpunitPos> builder)
        {
            builder.ToTable("UserInOrgOpunitPos", "usr");
            builder.HasKey(x => x.Id);
            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId_FK);
            builder.HasOne<Organization>().WithMany().HasForeignKey(x => x.OrganizationId_FK);
            builder.HasOne<OperationUnit>().WithMany().HasForeignKey(x => x.OperationUnitId_FK);
            builder.HasOne<Position>().WithMany().HasForeignKey(x => x.PositionId_FK);
            builder.HasIndex(x => new { x.UserId_FK, x.OrganizationId_FK, x.OperationUnitId_FK, x.PositionId_FK }).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.UserId_FK).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.OrganizationId_FK).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.OperationUnitId_FK).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.PositionId_FK).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(5);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(6);
            builder.Property(x => x.IsDeleted).HasColumnOrder(7);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(8);

            builder.HasData(new UserInOrgOpunitPos { Id = 1, UserId_FK = 1, OrganizationId_FK = 1, OperationUnitId_FK = 1, PositionId_FK = 2 },//Mohajer
                            new UserInOrgOpunitPos { Id = 2, UserId_FK = 2, OrganizationId_FK = 1, OperationUnitId_FK = 2, PositionId_FK = 3 },//Moslemi
                            new UserInOrgOpunitPos { Id = 3, UserId_FK = 3, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 4 },//Moradi
                            new UserInOrgOpunitPos { Id = 4, UserId_FK = 4, OrganizationId_FK = 1, OperationUnitId_FK = 15, PositionId_FK = 6 },//Basiri
                            new UserInOrgOpunitPos { Id = 5, UserId_FK = 5, OrganizationId_FK = 1, OperationUnitId_FK = 13, PositionId_FK = 6 },//Dadras
                            new UserInOrgOpunitPos { Id = 6, UserId_FK = 6, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 6 },//Aghababaei
                            new UserInOrgOpunitPos { Id = 7, UserId_FK = 7, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 6 },//Khojasteh
                            new UserInOrgOpunitPos { Id = 8, UserId_FK = 8, OrganizationId_FK = 1, OperationUnitId_FK = 7, PositionId_FK = 8 },//Eskandari
                            new UserInOrgOpunitPos { Id = 9, UserId_FK = 9, OrganizationId_FK = 1, OperationUnitId_FK = 4, PositionId_FK = 6 },//Vafaeinia
                            new UserInOrgOpunitPos { Id = 10, UserId_FK = 10, OrganizationId_FK = 1, OperationUnitId_FK = 5, PositionId_FK = 6 },//Hoseini
                            new UserInOrgOpunitPos { Id = 11, UserId_FK = 11, OrganizationId_FK = 1, OperationUnitId_FK = 9, PositionId_FK = 6 },//Vesali
                            new UserInOrgOpunitPos { Id = 12, UserId_FK = 12, OrganizationId_FK = 1, OperationUnitId_FK = 12, PositionId_FK = 6 },//Akhani
                            new UserInOrgOpunitPos { Id = 13, UserId_FK = 13, OrganizationId_FK = 1, OperationUnitId_FK = 14, PositionId_FK = 8 },//Jalayer
                            new UserInOrgOpunitPos { Id = 14, UserId_FK = 14, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 6 },//Yusefi
                            new UserInOrgOpunitPos { Id = 15, UserId_FK = 15, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 7 },//Sepandasa
                            new UserInOrgOpunitPos { Id = 16, UserId_FK = 16, OrganizationId_FK = 1, OperationUnitId_FK = 10, PositionId_FK = 8 },//J.Azaresh
                            new UserInOrgOpunitPos { Id = 17, UserId_FK = 17, OrganizationId_FK = 1, OperationUnitId_FK = 15, PositionId_FK = 18 },//Kouchaki
                            new UserInOrgOpunitPos { Id = 18, UserId_FK = 18, OrganizationId_FK = 1, OperationUnitId_FK = 15, PositionId_FK = 17 },//Aghakasiri
                            new UserInOrgOpunitPos { Id = 19, UserId_FK = 19, OrganizationId_FK = 1, OperationUnitId_FK = 15, PositionId_FK = 10 },//R.Jafari
                            new UserInOrgOpunitPos { Id = 20, UserId_FK = 20, OrganizationId_FK = 1, OperationUnitId_FK = 15, PositionId_FK = 10 },//Naseri
                            new UserInOrgOpunitPos { Id = 21, UserId_FK = 21, OrganizationId_FK = 1, OperationUnitId_FK = 6, PositionId_FK = 8 },//M.Jafari
                            new UserInOrgOpunitPos { Id = 22, UserId_FK = 22, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 8 },//Aziznezhad
                            new UserInOrgOpunitPos { Id = 23, UserId_FK = 23, OrganizationId_FK = 1, OperationUnitId_FK = 12, PositionId_FK = 8 },//R.Azaresh
                            new UserInOrgOpunitPos { Id = 24, UserId_FK = 24, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 10 },//Khanmohammadi
                            new UserInOrgOpunitPos { Id = 25, UserId_FK = 25, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 4 },//Asadollahi
                            new UserInOrgOpunitPos { Id = 26, UserId_FK = 26, OrganizationId_FK = 1, OperationUnitId_FK = 13, PositionId_FK = 9 },//Ehteshamzadeh
                            new UserInOrgOpunitPos { Id = 27, UserId_FK = 27, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 10 },//Shamloo
                            new UserInOrgOpunitPos { Id = 28, UserId_FK = 28, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 9 },//Y.Jafari
                            new UserInOrgOpunitPos { Id = 29, UserId_FK = 29, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 10 },//Safizadeh
                            new UserInOrgOpunitPos { Id = 30, UserId_FK = 30, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 9 },//Ziraki
                            new UserInOrgOpunitPos { Id = 31, UserId_FK = 31, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 9 },//Ketabi
                            new UserInOrgOpunitPos { Id = 32, UserId_FK = 32, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 11 },//Adaei
                            new UserInOrgOpunitPos { Id = 33, UserId_FK = 33, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 11 },//Hadi
                            new UserInOrgOpunitPos { Id = 34, UserId_FK = 34, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 9 },//Hemmati
                            new UserInOrgOpunitPos { Id = 35, UserId_FK = 35, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 9 },//Shokri
                            new UserInOrgOpunitPos { Id = 36, UserId_FK = 36, OrganizationId_FK = 1, OperationUnitId_FK = 14, PositionId_FK = 10 },//Tabasi
                            new UserInOrgOpunitPos { Id = 37, UserId_FK = 37, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 12 },//Tavakoli
                            new UserInOrgOpunitPos { Id = 38, UserId_FK = 38, OrganizationId_FK = 1, OperationUnitId_FK = 9, PositionId_FK = 10 },//Sharafifard
                            new UserInOrgOpunitPos { Id = 39, UserId_FK = 39, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 10 },//Boujarnezhad
                            new UserInOrgOpunitPos { Id = 40, UserId_FK = 40, OrganizationId_FK = 1, OperationUnitId_FK = 14, PositionId_FK = 10 },//Rezaei
                            new UserInOrgOpunitPos { Id = 41, UserId_FK = 41, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 10 },//Afshar
                            new UserInOrgOpunitPos { Id = 42, UserId_FK = 42, OrganizationId_FK = 1, OperationUnitId_FK = 1, PositionId_FK = 18 },//Beigi
                            new UserInOrgOpunitPos { Id = 43, UserId_FK = 43, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 10 },//Norouzi
                            new UserInOrgOpunitPos { Id = 44, UserId_FK = 44, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 9 },//Kafi
                            new UserInOrgOpunitPos { Id = 45, UserId_FK = 45, OrganizationId_FK = 1, OperationUnitId_FK = 4, PositionId_FK = 10 },//Mosaferi
                            new UserInOrgOpunitPos { Id = 46, UserId_FK = 46, OrganizationId_FK = 1, OperationUnitId_FK = 8, PositionId_FK = 10 },//Mandegar
                            new UserInOrgOpunitPos { Id = 47, UserId_FK = 47, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 9 },//Mahmoudi
                            new UserInOrgOpunitPos { Id = 48, UserId_FK = 48, OrganizationId_FK = 1, OperationUnitId_FK = 7, PositionId_FK = 10 },//Marookian
                            new UserInOrgOpunitPos { Id = 49, UserId_FK = 49, OrganizationId_FK = 1, OperationUnitId_FK = 12, PositionId_FK = 10 },//Golmohammadi
                            new UserInOrgOpunitPos { Id = 50, UserId_FK = 50, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 10 },//Abedini
                            new UserInOrgOpunitPos { Id = 51, UserId_FK = 51, OrganizationId_FK = 1, OperationUnitId_FK = 3, PositionId_FK = 10 },//Zare
                            new UserInOrgOpunitPos { Id = 52, UserId_FK = 52, OrganizationId_FK = 1, OperationUnitId_FK = 11, PositionId_FK = 10 },//Nemati
                            new UserInOrgOpunitPos { Id = 53, UserId_FK = 53, OrganizationId_FK = 1, OperationUnitId_FK = 7, PositionId_FK = 10 },//Saraei
                            new UserInOrgOpunitPos { Id = 54, UserId_FK = 54, OrganizationId_FK = 1, OperationUnitId_FK = 5, PositionId_FK = 10 },//Malih
                            new UserInOrgOpunitPos { Id = 55, UserId_FK = 55, OrganizationId_FK = 1, OperationUnitId_FK = 4, PositionId_FK = 10 }//Majloubi
                            );
        }
    }
}
