namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.GetServicePhase
{
    public class ResultGetServicePhaseFullListDto
    {
        public List<RequestGetServicePhaseFullListDto> srvPhaseList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
