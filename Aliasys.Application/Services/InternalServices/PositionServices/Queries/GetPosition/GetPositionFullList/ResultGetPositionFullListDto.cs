namespace Aliasys.Application.Services.InternalServices.PositionServices.Queries.GetPosition.GetPositionFullList
{
    public class ResultGetPositionFullListDto
    {
        public List<RequestGetPositionFullListDto> positionList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
