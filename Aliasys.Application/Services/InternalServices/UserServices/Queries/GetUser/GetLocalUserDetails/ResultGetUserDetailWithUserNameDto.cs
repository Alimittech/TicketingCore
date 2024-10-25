namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserDetails
{
    public class ResultGetUserDetailWithUserNameDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtentionNumber { get; set; }
        public int? PersonCode { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }
        public string Organization { get; set; }
        public int OperationUnitId { get; set; }
        public string OperationUnit { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string? UserRollName { get; set; }
        public string? ImageName { get; set; }
    }
}
