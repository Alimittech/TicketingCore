using Aliasys.Domain.Entities.ServiceEntities;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.GetServiceRequestType.GetServiceReqTypeFullList
{
    public class RequestGetServiceReqTypeFullListDto
    {
        public int Id { get; set; }
        public RequestType RequestType { get; set; }
        public string Name { get; set; }
        public string BriefName { get; set; }
    }
}
