using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.OrganizationEntities
{
    public class Organization : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string DomainName { get; set; }
        public int ParentId_FK { get; set; }
        public int RegionId_FK { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
