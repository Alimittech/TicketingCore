using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Commands.CreateSrvReqLifeCycle;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetAllReqLifeCycleWithReqId;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServiceRequestLifeCycleFacad
    {
        IGetService<ResultGetAllReqLifeCycleDto, int> GetAllReqLifeCycleWithReqIdService { get; }
        IGetService<ResultGetReqLifeCycleDependencyDto, long> GetReqLifeCycleDependencyWithReqIdService { get; }
        ICreateService<int?, RequestCreateSrvReqLifeCycleDto> CreateServiceReqLifeCycleService { get; }
    }
}
