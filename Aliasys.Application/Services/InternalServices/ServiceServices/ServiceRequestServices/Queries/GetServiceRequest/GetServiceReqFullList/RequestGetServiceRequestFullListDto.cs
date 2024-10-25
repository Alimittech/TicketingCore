using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetServiceReqFullList
{
    public class RequestGetServiceRequestFullListDto
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string OwnerName { get; set; }
        public string ProcessorName { get; set; }
        public string RequestNumber { get; set; }
        public string ServiceCategory { get; set; }
        public string ServiceRequestType { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string StateName { get; set; }
        public string PhaseName { get; set; }
    }
}
