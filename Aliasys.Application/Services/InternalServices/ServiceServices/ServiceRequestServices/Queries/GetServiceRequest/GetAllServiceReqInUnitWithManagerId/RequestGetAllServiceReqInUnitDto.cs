using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithManagerId
{
    public class RequestGetAllServiceReqInUnitDto
    {
        public int ManagerId { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}
