using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.CreateServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.DeleteServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.UpdateServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.FindServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.GetServiceRequestType.GetServiceReqTypeFullList;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.GetServiceRequestType.GetServiceReqTypeSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.FacadPatterns
{
    public class ServiceRequestTypeFacad : IServiceRequestTypeFacad
    {
        private readonly IDataBaseContext _context;
        public ServiceRequestTypeFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindServiceRequestTypeDto, int> _findServiceRequestTypeWithIdService;
        public IFindService<ResultFindServiceRequestTypeDto, int> FindServiceRequestTypeWithIdService
        {
            get
            {
                return _findServiceRequestTypeWithIdService = _findServiceRequestTypeWithIdService ?? new FindServiceRequestTypeWithIdService(_context);
            }
        }

        private IFindService<ResultFindServiceRequestTypeDto, string> _findServiceRequestTypeWithNameService;
        public IFindService<ResultFindServiceRequestTypeDto, string> FindServiceRequestTypeWithNameService
        {
            get
            {
                return _findServiceRequestTypeWithNameService = _findServiceRequestTypeWithNameService ?? new FindServiceRequestTypeWithNameService(_context);
            }
        }

        private IGetService<ResultGetServiceReqTypeFullListDto, PaginationDto> _getServiceRequestTypeFullListService;
        public IGetService<ResultGetServiceReqTypeFullListDto, PaginationDto> GetServiceRequestTypeFullListService
        {
            get
            {
                return _getServiceRequestTypeFullListService = _getServiceRequestTypeFullListService ?? new GetServiceRequestTypeFullListService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getServiceRequestTypeSelectListService;
        public IGetService<List<SelectListItem>> GetServiceRequestTypeSelectListService
        {
            get
            {
                return _getServiceRequestTypeSelectListService = _getServiceRequestTypeSelectListService ?? new GetServiceRequestTypeSelectListService(_context);
            }
        }

        private ICreateService<int?, RequestCreateServiceRequestTypeDto> _createServiceRequestTypeService;
        public ICreateService<int?, RequestCreateServiceRequestTypeDto> CreateServiceRequestTypeService
        {
            get
            {
                return _createServiceRequestTypeService = _createServiceRequestTypeService ?? new CreateServiceRequestTypeService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateServiceRequestTypeDto> _updateServiceRequestTypeService;
        public IUpdateService<int?, RequestUpdateServiceRequestTypeDto> UpdateServiceRequestTypeService
        {
            get
            {
                return _updateServiceRequestTypeService = _updateServiceRequestTypeService ?? new UpdateServiceRequestTypeService(_context);
            }
        }

        private IDeleteService _deleteServiceRequestTypeService;
        public IDeleteService DeleteServiceRequestTypeService
        {
            get
            {
                return _deleteServiceRequestTypeService = _deleteServiceRequestTypeService ?? new DeleteServiceRequestTypeService(_context);
            }
        }
    }
}
