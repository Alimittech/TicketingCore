using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.CreateServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.DeleteServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.UpdateServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.FindServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.GetServiceState;
using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.FacadPatterns
{
    public class ServiceStateFacad : IServiceStateFacad
    {
        private readonly IDataBaseContext _context;
        public ServiceStateFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindServiceStateDto, int> _findServiceStateWithIdService;
        public IFindService<ResultFindServiceStateDto, int> FindServiceStateWithIdService
        {
            get
            {
                return _findServiceStateWithIdService = _findServiceStateWithIdService ?? new FindServiceStateWithIdService(_context);
            }
        }

        private IFindService<ResultFindServiceStateDto, string> _findServiceStateWithNameService;
        public IFindService<ResultFindServiceStateDto, string> FindServiceStateWithNameService
        {
            get
            {
                return _findServiceStateWithNameService = _findServiceStateWithNameService ?? new FindServiceStateWithNameService(_context);
            }
        }

        private IGetService<ResultGetServiceStateFullListDto, PaginationDto> _getServiceStateFullListService;
        public IGetService<ResultGetServiceStateFullListDto, PaginationDto> GetServiceStateFullListService
        {
            get
            {
                return _getServiceStateFullListService = _getServiceStateFullListService ?? new GetServiceStateFullListService(_context);
            }
        }

        private IGetService<List<ResultGetServiceStateAllDto>> _getServiceStateAllService;
        public IGetService<List<ResultGetServiceStateAllDto>> GetServiceStateAllService
        {
            get
            {
                return _getServiceStateAllService = _getServiceStateAllService ?? new GetServiceStateAllService(_context);
            }
        }

        private ICreateService<int?, RequestCreateServiceStateDto> _createServiceStateService;
        public ICreateService<int?, RequestCreateServiceStateDto> CreateServiceStateService
        {
            get
            {
                return _createServiceStateService = _createServiceStateService ?? new CreateServiceStateService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateServiceStateDto> _updateServiceStateService;
        public IUpdateService<int?, RequestUpdateServiceStateDto> UpdateServiceStateService
        {
            get
            {
                return _updateServiceStateService = _updateServiceStateService ?? new UpdateServiceStateService(_context);
            }
        }

        private IDeleteService _deleteServiceStateService;
        public IDeleteService DeleteServiceStateService
        {
            get
            {
                return _deleteServiceStateService = _deleteServiceStateService ?? new DeleteServiceStateService(_context);
            }
        }
    }
}
