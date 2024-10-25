using Aliasys.Domain.Entities.ServiceEntities;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetAllReqLifeCycleWithReqId
{
    public class RequestGetAllReqLifeCycleDto
    {
        public long Id { get; set; }
        public long ServiceRequestId { get; set; }
        public string ServiceStateName { get; set; }
        public string ServicePhaseName { get; set; }
        public ProcessAction ProcessAction { get; set; }
        public string RootCause { get; set; }
        public string SubCause { get; set; }
        public string Description { get; set; }
        public string? AttachmentPath { get; set; }
        public string ProcessUserDisplayName { get; set; }
        public string AssignedUserDisplayName { get; set; }
    }
}
