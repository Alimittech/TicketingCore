using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupFullList
{
    public class RequestGetUserInGroupFullListDto
    {
        public PaginationDto PaginationDto { get; set; }
        public int UserGroupId { get; set; }
    }
}
