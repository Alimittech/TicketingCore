using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceCauseCategory : BaseEntity<int>
    {
        public override int Id { get; set; }
        public int ServiceCategoryId_FK { get; set; }
        public int ServiceRootCauseId_FK { get; set; }
        public int ServiceSubCauseId_FK { get; set; }
    }
}
