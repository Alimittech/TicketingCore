namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.FindUserInGroup
{
    public class ResultFindUserInGroupDto
    {
        public int Id { get; set; }
        public int UserGroupId_FK { get; set; }
        public string UserGroupName { get; set; }
        public int UserId_FK { get; set; }
    }
}
