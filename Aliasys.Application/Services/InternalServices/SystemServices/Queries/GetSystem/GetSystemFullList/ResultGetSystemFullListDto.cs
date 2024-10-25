namespace Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemFullList
{
    public class ResultGetSystemFullListDto
    {
        public List<RequestGetSystemFullListDto> sysList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
