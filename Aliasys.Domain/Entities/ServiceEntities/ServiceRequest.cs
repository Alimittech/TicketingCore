using Aliasys.Domain.Entities.BaseEntities;

namespace Aliasys.Domain.Entities.ServiceEntities
{
    public class ServiceRequest : BaseEntity<long>
    {
        public override long Id { get; set; }
        public string RequestNumber { get; set; }//ERP-TT-20230809-000001
        public int OwnerUserId_FK { get; set; }
        public int ServiceCategoryId_FK { get; set; }
        public int ServiceRequestTypeId_FK { get; set; }
        public ServicePriority ServicePriority { get; set; }
        public DateTime OccurDateTime { get; set; }
        public bool ServiceAffected { get; set; }
        public ImpactOn ImpactOn { get; set; }
        public string Title { get; set; }
    }

    public enum ServicePriority
    {
        L1 = 1,
        L2 = 2,
        L3 = 3,
        L4 = 4,
    }

    public enum ImpactOn//تأثیرپذیری
    {
        User = 1,
        Unit = 2,
        Company = 3
    }
}
