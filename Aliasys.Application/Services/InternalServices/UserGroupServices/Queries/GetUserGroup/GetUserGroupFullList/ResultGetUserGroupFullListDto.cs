namespace Aliasys.Application.Services.InternalServices.UserGroupServices.Queries.GetUserGroup.GetUserGroupFullList
{
    public class ResultGetUserGroupFullListDto
    {
        public List<RequestGetUserGroupFullListDto> userGroupList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
