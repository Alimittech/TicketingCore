namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetServiceReqFullList
{
    public class ResultGetServiceRequestFullListDto
    {
        public List<RequestGetServiceRequestFullListDto> srvRequestList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
