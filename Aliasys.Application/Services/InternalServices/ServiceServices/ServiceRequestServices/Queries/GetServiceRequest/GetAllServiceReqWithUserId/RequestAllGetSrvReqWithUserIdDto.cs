using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithUserId
{
    public class RequestAllGetSrvReqWithUserIdDto
    {
        public int UserId { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}
