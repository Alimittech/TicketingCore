namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.GetServiceCategory.GetServiceCatFullList
{
    public class ResultGetServiceCategoryFullListDto
    {
        public List<RequestGetServiceCategoryFullListDto> srvCategoryList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
