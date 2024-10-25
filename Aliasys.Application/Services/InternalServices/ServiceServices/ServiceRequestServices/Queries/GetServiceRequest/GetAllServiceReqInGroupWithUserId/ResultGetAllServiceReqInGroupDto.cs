namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqInGroupWithUserId
{
    public class ResultGetAllServiceReqInGroupDto
    {
        public List<ResGetAllServiceReqInGroupDto> srvRequestList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
