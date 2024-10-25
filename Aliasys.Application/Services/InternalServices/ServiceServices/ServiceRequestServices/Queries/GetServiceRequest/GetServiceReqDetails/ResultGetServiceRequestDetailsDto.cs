using Aliasys.Domain.Entities.ServiceEntities;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetServiceReqDetails
{
    public class ResultGetServiceRequestDetailsDto
    {
        public long Id { get; set; }
        public string RequestNumber { get; set; }
        public int OwnerUserId_FK { get; set; }
        public int ServiceCategoryId_FK { get; set; }
        public int ServiceRequestTypeId_FK { get; set; }
        public ServicePriority ServicePriority { get; set; }
        public DateTime OccurDateTime { get; set; }
        public bool ServiceAffected { get; set; }
        public ImpactOn ImpactOn { get; set; }
        public string Title { get; set; }
    }
}
