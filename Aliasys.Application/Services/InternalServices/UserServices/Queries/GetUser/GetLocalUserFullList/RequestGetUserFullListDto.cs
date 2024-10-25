namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserFullList
{
    public class RequestGetUserFullListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtentionNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
