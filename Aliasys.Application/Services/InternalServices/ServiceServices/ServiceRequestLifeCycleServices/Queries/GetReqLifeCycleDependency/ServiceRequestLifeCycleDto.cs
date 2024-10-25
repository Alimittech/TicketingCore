using Aliasys.Domain.Entities.ServiceEntities;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency
{
    public class ServiceRequestLifeCycleDto
    {
        public long ReqLifeCycleId { get; set; }
        public string StateName { get; set; }
        public string PhaseName { get; set; }
        public ProcessAction ProcessAction { get; set; }
        public int? RootCause { get; set; }
        public int? SubCause { get; set; }
        public String? Description { get; set; }
        public string? AttachmentFileName { get; set; }
        public string ProcessUser { get; set; }
        public string AssignedUser { get; set; }
        public string? AssignedGroup { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
