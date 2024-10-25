using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqInGroupWithUserId
{
    public class RequestGetAllServiceReqInGroupDto
    {
        public int UserId { get; set; }
        public PaginationDto Pagination { get; set; }
        public bool ShowAllRequest { get; set; }
        public bool ShowSearchkey { get; set; }

    }
}
