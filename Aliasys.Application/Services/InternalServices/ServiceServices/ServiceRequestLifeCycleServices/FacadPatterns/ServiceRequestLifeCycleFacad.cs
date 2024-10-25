using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Commands.CreateSrvReqLifeCycle;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetAllReqLifeCycleWithReqId;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency;
using Aliasys.Infrastructure.ExternalServices.NotificationServices;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.FacadPatterns
{
    public class ServiceRequestLifeCycleFacad : IServiceRequestLifeCycleFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IUserFacad _userFacad;
        private readonly INotificationService<Task, RequestEmailServiceDto> _notificationService;
        private readonly IServiceRequestFacad _serviceRequestFacad;
        public ServiceRequestLifeCycleFacad(IDataBaseContext context, INotificationService<Task, RequestEmailServiceDto> notificationService, IUserFacad userFacad, IServiceRequestFacad serviceRequestFacad)
        {
            _context = context;
            _notificationService = notificationService;
            _userFacad = userFacad;
            _serviceRequestFacad = serviceRequestFacad;
        }

        private IGetService<ResultGetAllReqLifeCycleDto, int> _getAllReqLifeCycleWithReqIdService;
        public IGetService<ResultGetAllReqLifeCycleDto, int> GetAllReqLifeCycleWithReqIdService
        {
            get
            {
                return _getAllReqLifeCycleWithReqIdService = _getAllReqLifeCycleWithReqIdService ?? new GetAllReqLifeCycleWithReqIdService(_context);
            }
        }

        private IGetService<ResultGetReqLifeCycleDependencyDto, long> _getReqLifeCycleDependencyWithReqIdService;
        public IGetService<ResultGetReqLifeCycleDependencyDto, long> GetReqLifeCycleDependencyWithReqIdService
        {
            get
            {
                return _getReqLifeCycleDependencyWithReqIdService = _getReqLifeCycleDependencyWithReqIdService ?? new GetReqLifeCycleDependencyWithReqIdService(_context);
            }
        }

        private ICreateService<int?, RequestCreateSrvReqLifeCycleDto> _createServiceReqLifeCycleService;
        public ICreateService<int?, RequestCreateSrvReqLifeCycleDto> CreateServiceReqLifeCycleService
        {
            get
            {
                return _createServiceReqLifeCycleService = _createServiceReqLifeCycleService ?? new CreateServiceReqLifeCycleService(_context,
                    _userFacad, _notificationService,_serviceRequestFacad);
            }
        }
    }
}
