using Aliasys.Domain.Entities.ServiceEntities;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.FindServiceRequest
{
    public class ResultFindServiceRequestDto
    {
        public long Id { get; set; }
        public string RequestNumber { get; set; }
        public int OwnerUserId { get; set; }
        public int ServiceCategoryId { get; set; }
        public int ServiceRequestTypeId { get; set; }
        public ServicePriority ServicePriority { get; set; }
        public DateTime OccurDateTime { get; set; }
        public bool ServiceAffected { get; set; }
        public ImpactOn ImpactOn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? AttachmentPath { get; set; }
    }
}
