
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aliasys.Persistence.Configurations.UserConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "usr");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.UserName).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(1);
            builder.Property(x => x.DisplayName).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(2);
            builder.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(100)").HasColumnOrder(3);
            builder.Property(x => x.PhoneNumber).IsRequired().HasColumnType("nvarchar(11)").HasColumnOrder(4);
            builder.Property(x => x.ExtentionNumber).IsRequired().HasColumnType("nvarchar(3)").HasColumnOrder(5);
            builder.Property(x => x.PersonCode).HasColumnOrder(6);
            builder.Property(x => x.IsActive).IsRequired().HasColumnOrder(7);
            builder.Property(x => x.ImageName).HasColumnOrder(8);
            builder.Property(x => x.CreatedDateTime).HasColumnOrder(9);
            builder.Property(x => x.UpdatedDateTime).HasColumnOrder(10);
            builder.Property(x => x.IsDeleted).HasColumnOrder(11);
            builder.Property(x => x.DeletedDateTime).HasColumnOrder(12);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(new User { Id = 1, UserName = "e.mohajer", DisplayName = "Mohajer, Ehsan", Email = "e.mohajer@aliasys.co", PhoneNumber = "09121863720", ExtentionNumber = "500", PersonCode = 28, ImageName="028", IsActive = true },
                            new User { Id = 2, UserName = "a.moslemi", DisplayName = "Moslemi, Amir", Email = "a.moslemi@aliasys.co", PhoneNumber = "09126220378", ExtentionNumber = "700", PersonCode = 3, ImageName = "003", IsActive = true },
                            new User { Id = 3, UserName = "ed.moradi", DisplayName = "Moradi, Edris", Email = "ed.moradi@aliasys.co", PhoneNumber = "09126353086", ExtentionNumber = "750", PersonCode = 24, IsActive = true },
                            new User { Id = 4, UserName = "m.basiri", DisplayName = "Basiri, Mohammad", Email = "m.basiri@aliasys.co", PhoneNumber = "09122584866", ExtentionNumber = "950", PersonCode = 72, ImageName = "072", IsActive = true },
                            new User { Id = 5, UserName = "m.dadras", DisplayName = "Dadras, Majid", Email = "m.dadras@aliasys.co", PhoneNumber = "09125469645", ExtentionNumber = "200", PersonCode = 94, ImageName = "094", IsActive = true },
                            new User { Id = 6, UserName = "a.aghababaei", DisplayName = "Aghababaei, Ali", Email = "a.aghababaei@aliasys.co", PhoneNumber = "09121724565", ExtentionNumber = "300", PersonCode = 15, ImageName = "015", IsActive = true },
                            new User { Id = 7, UserName = "a.khojasteh", DisplayName = "Afsaneh Khojasteh", Email = "a.khojasteh@aliasys.co", PhoneNumber = "09123907133", ExtentionNumber = "800", PersonCode = 19, ImageName = "019", IsActive = true },
                            new User { Id = 8, UserName = "p.eskandari", DisplayName = "Eskandari, Pegah", Email = "p.eskandari@aliasys.co", PhoneNumber = "09125774563", ExtentionNumber = "610", PersonCode = 16, ImageName = "016", IsActive = true },
                            new User { Id = 9, UserName = "m.vafaeinia", DisplayName = "Vafaei Nia, Mojgan", Email = "m.vafaeinia@aliasys.co", PhoneNumber = "09122993517", ExtentionNumber = "105", PersonCode = 117, ImageName = "117", IsActive = true },
                            new User { Id = 10, UserName = "h.hoseini", DisplayName = "Hoseini, Hamidreza ", Email = "h.hoseini@aliasys.co", PhoneNumber = "09352566788", ExtentionNumber = "400", PersonCode = 39, ImageName = "039", IsActive = true },
                            new User { Id = 11, UserName = "m.vesali", DisplayName = "Vesali, Mostafa", Email = "m.vesali@aliasys.co", PhoneNumber = "09127708514", ExtentionNumber = "660", PersonCode = 118, IsActive = true },
                            new User { Id = 12, UserName = "m.akhani", DisplayName = "Akhani, Malek", Email = "m.akhani@aliasys.co", PhoneNumber = "09127617152", ExtentionNumber = "450", PersonCode = 95, ImageName = "095", IsActive = true },
                            new User { Id = 13, UserName = "H.jalayer", DisplayName = "Jalayer, Hasti", Email = "H.jalayer@aliasys.co", PhoneNumber = "09123279790", ExtentionNumber = "605", PersonCode = 22, ImageName = "022", IsActive = true },
                            new User { Id = 14, UserName = "r.yousefi", DisplayName = "Yousefi, Rouhollah", Email = "r.yousefi@aliasys.co", PhoneNumber = "09126469135", ExtentionNumber = "550", PersonCode = 120, ImageName = "120", IsActive = true },
                            new User { Id = 15, UserName = "b.sepandasa", DisplayName = "Sepandasa, Taghi", Email = "b.sepandasa@aliasys.co", PhoneNumber = "09121435662", ExtentionNumber = "310", PersonCode = 14, ImageName = "014", IsActive = true },
                            new User { Id = 16, UserName = "j.azaresh", DisplayName = "Azaresh, Javad", Email = "j.azaresh@aliasys.co", PhoneNumber = "09396080127", ExtentionNumber = "480", PersonCode = 44, ImageName = "044", IsActive = true },
                            new User { Id = 17, UserName = "m.kouchaki", DisplayName = "Kouchaki, Mehdi", Email = "m.kouchaki@aliasys.co", PhoneNumber = "09123185463", ExtentionNumber = "965", PersonCode = 106, ImageName = "106", IsActive = true },
                            new User { Id = 18, UserName = "h.aghakasiri", DisplayName = "Aghakasiri, Hamidreza", Email = "h.aghakasiri@aliasys.co", PhoneNumber = "09126108395", ExtentionNumber = "960", PersonCode = 115, ImageName = "115", IsActive = true },
                            new User { Id = 19, UserName = "r.jafari", DisplayName = "Jafari, Roghayeh", Email = "r.jafari@aliasys.co", PhoneNumber = "09101744763", ExtentionNumber = "575", PersonCode = 109, ImageName = "109", IsActive = true },
                            new User { Id = 20, UserName = "e.naseri", DisplayName = "Naseri, Erfan", Email = "e.naseri@aliasys.co", PhoneNumber = "09025367966", ExtentionNumber = "957", PersonCode = 119, ImageName = "119", IsActive = true },
                            new User { Id = 21, UserName = "m.jafari", DisplayName = "Jafari, Mohammad", Email = "m.jafari@aliasys.co", PhoneNumber = "09127186105", ExtentionNumber = "823", PersonCode = 32, ImageName = "032", IsActive = true },
                            new User { Id = 22, UserName = "r.aziznezhad", DisplayName = "Aziznezhad, Ronak", Email = "r.aziznezhad@aliasys.co", PhoneNumber = "09124691582", ExtentionNumber = "560", PersonCode = 2, ImageName = "002", IsActive = true },
                            new User { Id = 23, UserName = "r.azaresh", DisplayName = "Azaresh, Reza", Email = "r.azaresh@aliasys.co", PhoneNumber = "09126704771", ExtentionNumber = "430", PersonCode = 10, ImageName = "010", IsActive = true },
                            new User { Id = 24, UserName = "f.khanmohammadi", DisplayName = "Khanmohammadi, Fatemeh", Email = "f.khanmohammadi@aliasys.co", PhoneNumber = "09107877517", ExtentionNumber = "563", PersonCode = 11, ImageName = "011", IsActive = true },
                            new User { Id = 25, UserName = "a.asadollahi", DisplayName = "Asadollahi, Ardavan", Email = "a.asadollahi@aliasys.co", PhoneNumber = "09126955076", ExtentionNumber = "000", PersonCode = 13, IsActive = true },
                            new User { Id = 26, UserName = "s.ehteshamzadeh", DisplayName = "Ehteshamzadeh, Saman", Email = "s.ehteshamzadeh@aliasys.co", PhoneNumber = "09372423534", ExtentionNumber = "217", PersonCode = 18, ImageName = "018", IsActive = true },
                            new User { Id = 27, UserName = "a.shamloo", DisplayName = "Shamloo, Alireza", Email = "a.shamloo@aliasys.co", PhoneNumber = "09128921450", ExtentionNumber = "821", PersonCode = 21, ImageName = "021", IsActive = true },
                            new User { Id = 28, UserName = "y.jafari", DisplayName = "Jafari, Yousef", Email = "y.jafari@aliasys.co@aliasys.co", PhoneNumber = "09120762408", ExtentionNumber = "967", PersonCode = 25, ImageName = "025", IsActive = true },
                            new User { Id = 29, UserName = "m.safizadeh", DisplayName = "Safizadeh, Mohammad", Email = "m.safizadeh@aliasys.co", PhoneNumber = "09120755724", ExtentionNumber = "831", PersonCode = 27, IsActive = false },
                            new User { Id = 30, UserName = "sh.ziraki", DisplayName = "Ziraki, Shahab", Email = "sh.ziraki@aliasys.co", PhoneNumber = "09304975026", ExtentionNumber = "325", PersonCode = 30, ImageName = "030", IsActive = true },
                            new User { Id = 31, UserName = "f.ketabi", DisplayName = "Ketabi, Faraz", Email = "f.ketabi@aliasys.co", PhoneNumber = "09126988850", ExtentionNumber = "333", PersonCode = 31, ImageName = "031", IsActive = true },
                            new User { Id = 32, UserName = "e.adaei", DisplayName = "Adaei, Elnaz", Email = "e.adaei@aliasys.co", PhoneNumber = "09129358259", ExtentionNumber = "825", PersonCode = 34, ImageName = "034", IsActive = true },
                            new User { Id = 33, UserName = "s.hadi", DisplayName = "Hadi, Sajad", Email = "s.hadi@aliasys.co", PhoneNumber = "09126365952", ExtentionNumber = "833", PersonCode = 35, ImageName = "035", IsActive = true },
                            new User { Id = 34, UserName = "b.hemmati", DisplayName = "Hemmati, Behroz", Email = "b.hemmati@aliasys.co", PhoneNumber = "09120581940", ExtentionNumber = "317", PersonCode = 37, ImageName = "037", IsActive = true },
                            new User { Id = 35, UserName = "f.shokri", DisplayName = "Shokri, Farid", Email = "f.shokri@aliasys.co", PhoneNumber = "09127767506", ExtentionNumber = "321", PersonCode = 43, ImageName = "043", IsActive = true },
                            new User { Id = 36, UserName = "p.tabasi", DisplayName = "Tabasi, Pegah", Email = "p.tabasi@aliasys.co", PhoneNumber = "09106708373", ExtentionNumber = "610", PersonCode = 48, ImageName = "048", IsActive = true },
                            new User { Id = 37, UserName = "f.tavakoli", DisplayName = "Tavakoli, Fatemeh", Email = "f.tavakoli@aliasys.co", PhoneNumber = "09380902891", ExtentionNumber = "815", PersonCode = 57, ImageName = "057", IsActive = true },
                            new User { Id = 38, UserName = "f.sharafifard", DisplayName = "Sharafifard, Faezeh", Email = "f.sharafifard@aliasys.co", PhoneNumber = "09129405474", ExtentionNumber = "671", PersonCode = 63, ImageName = "063", IsActive = true },
                            new User { Id = 39, UserName = "t.boujarnezhad", DisplayName = "Boujarnezhad, Tina", Email = "t.boujarnezhad@aliasys.co", PhoneNumber = "09354292249", ExtentionNumber = "573", PersonCode = 64, ImageName = "064", IsActive = true },
                            new User { Id = 40, UserName = "a.rezaei", DisplayName = "Rezaei, Anahita", Email = "a.rezaei@aliasys.co", PhoneNumber = "09111160272", ExtentionNumber = "621", PersonCode = 65, ImageName = "065", IsActive = true },
                            new User { Id = 41, UserName = "m.afshar", DisplayName = "Afshar, Mehrnaz", Email = "m.afshar@aliasys.co", PhoneNumber = "09125038412", ExtentionNumber = "819", PersonCode = 68, ImageName = "068", IsActive = true },
                            new User { Id = 42, UserName = "n.beygi", DisplayName = "Beygi, Narges", Email = "n.beygi@aliasys.co", PhoneNumber = "09124972631", ExtentionNumber = "115", PersonCode = 73, ImageName = "073", IsActive = true },
                            new User { Id = 43, UserName = "n.norouzi", DisplayName = "Norouzi, Negin", Email = "n.norouzi@aliasys.co", PhoneNumber = "09372571925", ExtentionNumber = "569", PersonCode = 75, ImageName = "075", IsActive = true },
                            new User { Id = 44, UserName = "f.kafi", DisplayName = "Kafi, Farzad", Email = "f.kafi@aliasys.co", PhoneNumber = "09384001555", ExtentionNumber = "329", PersonCode = 86, ImageName = "086", IsActive = true },
                            new User { Id = 45, UserName = "h.mosaferi", DisplayName = "Mosaferi, Hosien", Email = "h.mosaferi@aliasys.co", PhoneNumber = "09126095320", ExtentionNumber = "136", PersonCode = 92, ImageName = "092", IsActive = true },
                            new User { Id = 46, UserName = "m.mandegar", DisplayName = "Mandegar, Mahshid", Email = "m.mandegar@aliasys.co", PhoneNumber = "09123725132", ExtentionNumber = "580", PersonCode = 93, ImageName = "093", IsActive = true },
                            new User { Id = 47, UserName = "s.mahmoudi", DisplayName = "Mahmoudi, Saman", Email = "s.mahmoudi@aliasys.co", PhoneNumber = "09357436629", ExtentionNumber = "327", PersonCode = 96, ImageName = "096", IsActive = true },
                            new User { Id = 48, UserName = "p.marookian", DisplayName = "Marookian, Preni", Email = "p.marookian@aliasys.co", PhoneNumber = "09125175300", ExtentionNumber = "625", PersonCode = 99, ImageName = "099", IsActive = false },
                            new User { Id = 49, UserName = "a.golmohamadi", DisplayName = "Golmohamadi, Ali", Email = "a.golmohamadi@aliasys.co", PhoneNumber = "09909020595", ExtentionNumber = "435", PersonCode = 107, ImageName = "107", IsActive = true },
                            new User { Id = 50, UserName = "s.abedini", DisplayName = "Abedini, Samareh", Email = "s.abedini@aliasys.co", PhoneNumber = "09122680578", ExtentionNumber = "567", PersonCode = 111, ImageName = "111", IsActive = true },
                            new User { Id = 51, UserName = "a.zare", DisplayName = "Zare, Alireza", Email = "a.zare@aliasys.co", PhoneNumber = "09192018040", ExtentionNumber = "571", PersonCode = 112, ImageName = "112", IsActive = true },
                            new User { Id = 52, UserName = "a.nemati", DisplayName = "Nemati, Amir", Email = "a.nemati@aliasys.co", PhoneNumber = "09197219526", ExtentionNumber = "326", PersonCode = 113, ImageName = "113", IsActive = true },
                            new User { Id = 53, UserName = "m.saraei", DisplayName = "Saraei, Mahsa", Email = "m.saraei@aliasys.co", PhoneNumber = "09352522040", ExtentionNumber = "619", PersonCode = 114, ImageName = "114", IsActive = true },
                            new User { Id = 54, UserName = "s.malih", DisplayName = "Malih, Saeedeh", Email = "s.malih@aliasys.co", PhoneNumber = "09127565012", ExtentionNumber = "415", PersonCode = 121, IsActive = true },
                            new User { Id = 55, UserName = "m.majloubi", DisplayName = "Majloubi, Maral", Email = "m.majloubi@aliasys.co", PhoneNumber = "09123571140", ExtentionNumber = "119", PersonCode = 122, IsActive = true },
                            new User { Id = 56, UserName = "m.damirchi", DisplayName = "Damirchi, Maryam", Email = "m.damirchi@aliasys.co", PhoneNumber = "09918020123", ExtentionNumber = "831", PersonCode = 130, IsActive = true },
                            new User { Id = 57, UserName = "d.shamsi", DisplayName = "Shamsi, Donya", Email = "d.shamsi@aliasys.co", PhoneNumber = "09127989650", ExtentionNumber = "955", PersonCode = 126, IsActive = true },
                            new User { Id = 58, UserName = "h.basiri", DisplayName = "Basiri, Haniyeh", Email = "h.basiri@aliasys.co", PhoneNumber = "09333670980", ExtentionNumber = "575", PersonCode = 133, IsActive = true },
                            new User { Id = 59, UserName = "s.ghiasvand", DisplayName = "Ghiasvand, Samira", Email = "s.ghiasvand@aliasys.co", PhoneNumber = "09123365571", ExtentionNumber = "625", PersonCode = 135, IsActive = true },
                            new User { Id = 60, UserName = "n.serjouei", DisplayName = "Serjouei, Negar", Email = "n.serjouei@aliasys.co", PhoneNumber = "09397494105", ExtentionNumber = "819", PersonCode = 139, IsActive = true },
                            new User { Id = 61, UserName = "y.tabrizi", DisplayName = "Tabrizi, yasaman", Email = "y.tabrizi@aliasys.co", PhoneNumber = "09190199260", ExtentionNumber = "215", PersonCode = 138, IsActive = true }
                            );
        }
    }
}
