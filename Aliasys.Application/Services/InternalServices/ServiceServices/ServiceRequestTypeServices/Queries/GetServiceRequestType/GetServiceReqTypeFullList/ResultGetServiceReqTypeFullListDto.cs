namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.GetServiceRequestType.GetServiceReqTypeFullList
{
    public class ResultGetServiceReqTypeFullListDto
    {
        public List<RequestGetServiceReqTypeFullListDto> srvReqTypeList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
