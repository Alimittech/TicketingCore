using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.IServices.InternalServices;
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
using Aliasys.Infrastructure.ExternalServices.NotificationServices;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.FacadPatterns
{
    public class ServiceRequestFacad : IServiceRequestFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IServiceCategoryFacad _serviceCategoryFacad;
        private readonly IServiceRequestTypeFacad _serviceRequestTypeFacad;
        private readonly INotificationService<Task, RequestEmailServiceDto> _notificationService;
        private readonly IUserInGroupFacad _userInGroupFacad;
        private readonly IUserFacad _userFacad;
        public ServiceRequestFacad(IDataBaseContext context, 
            IServiceCategoryFacad serviceCategoryFacad,
            IServiceRequestTypeFacad serviceRequestTypeFacad, 
            INotificationService<Task, RequestEmailServiceDto> notificationService, 
            IUserInGroupFacad userInGroupFacad, 
            IUserFacad userFacad)
        {
            _context = context;
            _serviceCategoryFacad = serviceCategoryFacad;
            _serviceRequestTypeFacad = serviceRequestTypeFacad;
            _notificationService = notificationService;
            _userInGroupFacad = userInGroupFacad;
            _userFacad = userFacad;
        }
        private IFindService<ResultFindServiceRequestDto, long> _findServiceRequestWithIdService;
        public IFindService<ResultFindServiceRequestDto, long> FindServiceRequestWithIdService
        {
            get
            {
                return _findServiceRequestWithIdService = _findServiceRequestWithIdService ?? new FindServiceRequestWithIdService(_context);
            }
        }

        private IGetService<ResultGetServiceRequestFullListDto, PaginationDto> _getServiceRequestFullListService;
        public IGetService<ResultGetServiceRequestFullListDto, PaginationDto> GetServiceRequestFullListService
        {
            get
            {
                return _getServiceRequestFullListService = _getServiceRequestFullListService ?? new GetServiceRequestFullListService(_context);
            }
        }

        private IGetService<ResultGetAllServiceReqDto, RequestAllGetSrvReqWithUserIdDto> _getAllServiceReqWithUserIdService;
        public IGetService<ResultGetAllServiceReqDto, RequestAllGetSrvReqWithUserIdDto> GetAllServiceReqWithUserIdService
        {
            get
            {
                return _getAllServiceReqWithUserIdService = _getAllServiceReqWithUserIdService ?? new GetAllServiceReqWithUserIdService(_context);
            }
        }

        private IGetService<ResultGetAllServiceReqDto, RequestGetAllServiceReqInUnitDto> _getAllServiceReqWithManagerIdService;
        public IGetService<ResultGetAllServiceReqDto, RequestGetAllServiceReqInUnitDto> GetAllServiceReqWithManagerIdService
        {
            get
            {
                return _getAllServiceReqWithManagerIdService = _getAllServiceReqWithManagerIdService ?? new GetAllServiceReqWithManagerIdService(_context);
            }
        }

        private IGetService<ResultGetAllServiceReqInGroupDto, RequestGetAllServiceReqInGroupDto> _getAllServiceReqInGroupWithUserIdService;
        public IGetService<ResultGetAllServiceReqInGroupDto, RequestGetAllServiceReqInGroupDto> GetAllServiceReqInGroupWithUserIdService
        {
            get
            {
                return _getAllServiceReqInGroupWithUserIdService = _getAllServiceReqInGroupWithUserIdService ?? new GetAllServiceReqInGroupWithUserIdService(_context);
            }
        }

        private IGetService<ResultGetServiceRequestDetailsDto, long> _getServiceRequestDetailsService;
        public IGetService<ResultGetServiceRequestDetailsDto, long> GetServiceRequestDetailsService
        {
            get
            {
                return _getServiceRequestDetailsService = _getServiceRequestDetailsService ?? new GetServiceRequestDetailsService(_context);
            }
        }

        private ICreateService<long, RequestCreateServiceRequestDto> _createServiceRequestService;
        public ICreateService<long, RequestCreateServiceRequestDto> CreateServiceRequestService
        {
            get
            {
                return _createServiceRequestService = _createServiceRequestService ?? new CreateServiceRequestService
                    (_context, _serviceCategoryFacad, _serviceRequestTypeFacad, _notificationService , _userInGroupFacad, _userFacad);
            }
        }

        private IUpdateService<int?, RequestUpdateServiceRequestDto> _updateServiceRequestService;
        public IUpdateService<int?, RequestUpdateServiceRequestDto> UpdateServiceRequestService
        {
            get
            {
                return _updateServiceRequestService = _updateServiceRequestService ?? new UpdateServiceRequestService(_context);
            }
        }
    }
}
