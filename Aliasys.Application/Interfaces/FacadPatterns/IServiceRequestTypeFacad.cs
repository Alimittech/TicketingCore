using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.CreateServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.UpdateServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.FindServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.GetServiceRequestType.GetServiceReqTypeFullList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServiceRequestTypeFacad
    {
        IFindService<ResultFindServiceRequestTypeDto, int> FindServiceRequestTypeWithIdService { get; }
        IFindService<ResultFindServiceRequestTypeDto, string> FindServiceRequestTypeWithNameService { get; }
        IGetService<ResultGetServiceReqTypeFullListDto, PaginationDto> GetServiceRequestTypeFullListService { get; }
        IGetService<List<SelectListItem>> GetServiceRequestTypeSelectListService { get; }
        ICreateService<int?, RequestCreateServiceRequestTypeDto> CreateServiceRequestTypeService { get; }
        IUpdateService<int?, RequestUpdateServiceRequestTypeDto> UpdateServiceRequestTypeService { get; }
        IDeleteService DeleteServiceRequestTypeService { get; }
    }
}
