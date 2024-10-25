namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgFullList
{
    public class ResultGetOrganizationFullListDto
    {
        public List<RequestGetOrganizationFullListDto> orgList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
