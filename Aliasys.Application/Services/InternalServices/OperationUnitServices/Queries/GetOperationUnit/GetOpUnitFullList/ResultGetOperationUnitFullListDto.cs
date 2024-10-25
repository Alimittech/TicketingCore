namespace Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.GetOperationUnit.GetOpUnitFullList
{
    public class ResultGetOperationUnitFullListDto
    {
        public List<RequestGetOperationUnitFullListDto> opunitList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
