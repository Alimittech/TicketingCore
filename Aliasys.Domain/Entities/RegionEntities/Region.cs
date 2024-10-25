using Aliasys.Domain.Entities.OrganizationEntities;

namespace Aliasys.Domain.Entities.RegionEntities
{
    public class Region
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CapitalName { get; set; }
        public Continent ContinentName { get; set; }
        public string? Flag { get; set; }
    }

    public enum Continent
    {
        Africa = 0,
        America = 1,
        Asia = 2,
        Europe = 3,
        Oceania = 4,
    }
}
