using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Commands.CreateServiceRequest;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Commands.UpdateServiceRequest;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.FindServiceRequest;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqInGroupWithUserId;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithManagerId;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithUserId;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetServiceReqDetails;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetServiceReqFullList;
using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServiceRequestFacad
    {
        IFindService<ResultFindServiceRequestDto, long> FindServiceRequestWithIdService { get; }
        IGetService<ResultGetServiceRequestFullListDto, PaginationDto> GetServiceRequestFullListService { get; }
        IGetService<ResultGetAllServiceReqDto, RequestAllGetSrvReqWithUserIdDto> GetAllServiceReqWithUserIdService { get; }
        IGetService<ResultGetAllServiceReqDto, RequestGetAllServiceReqInUnitDto> GetAllServiceReqWithManagerIdService { get; }
        IGetService<ResultGetAllServiceReqInGroupDto, RequestGetAllServiceReqInGroupDto> GetAllServiceReqInGroupWithUserIdService { get; }
        IGetService<ResultGetServiceRequestDetailsDto, long> GetServiceRequestDetailsService { get; }
        ICreateService<long, RequestCreateServiceRequestDto> CreateServiceRequestService { get; }
        IUpdateService<int?, RequestUpdateServiceRequestDto> UpdateServiceRequestService { get; }
    }
}
