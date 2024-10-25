namespace Aliasys.Application.Services.InternalServices.UserDependency.Commands.CreateUserDependency
{
    public class RequestCreateUserDependencyDto
    {
        public int UserId_FK { get; set; }
        public int OrganizationId_FK { get; set; }
        public int OperationUnitId_FK { get; set; }
        public int PositionId_FK { get; set; }
    }
}
