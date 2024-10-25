namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupFullList
{
    public class ResultGetUserInGroupFullListDto
    {
        public List<GetUserInGroupFullListDto> userInGroupList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
