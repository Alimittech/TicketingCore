namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserFullList
{
    public class ResultGetUserFullListDto
    {
        public List<RequestGetUserFullListDto> userList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool As { get; set; }
    }
}
