namespace Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetUserDependency
{
    public class ResultGetUserDepDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OrganizationId { get; set; }
        public string Organization { get; set; }
        public int OperationUnitId { get; set; }
        public string OperationUnit { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
    }
}
