namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest
{
    public class ResultGetAllServiceReqDto
    {
        public List<ResGetAllServiceReqDto> srvRequestList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
