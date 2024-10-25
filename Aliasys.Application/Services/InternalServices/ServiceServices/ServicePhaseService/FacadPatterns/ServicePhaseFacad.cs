using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.CreateServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.DeleteServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.UpdateServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.FindServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.GetServicePhase;
using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.FacadPatterns
{
    public class ServicePhaseFacad : IServicePhaseFacad
    {
        private readonly IDataBaseContext _context;
        public ServicePhaseFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindServicePhaseDto, int> _findServicePhaseWithIdService;
        public IFindService<ResultFindServicePhaseDto, int> FindServicePhaseWithIdService
        {
            get
            {
                return _findServicePhaseWithIdService = _findServicePhaseWithIdService ?? new FindServicePhaseWithIdService(_context);
            }
        }

        private IFindService<ResultFindServicePhaseDto, string> _findServicePhaseWithNameService;
        public IFindService<ResultFindServicePhaseDto, string> FindServicePhaseWithNameService
        {
            get
            {
                return _findServicePhaseWithNameService = _findServicePhaseWithNameService ?? new FindServicePhaseWithNameService(_context);
            }
        }

        private IGetService<ResultGetServicePhaseFullListDto, PaginationDto> _getServicePhaseFullListService;
        public IGetService<ResultGetServicePhaseFullListDto, PaginationDto> GetServicePhaseFullListService
        {
            get
            {
                return _getServicePhaseFullListService = _getServicePhaseFullListService ?? new GetServicePhaseFullListService(_context);
            }
        }

        private IGetService<List<ResultGetServicePhaseAllDto>> _getServicePhaseAllService;
        public IGetService<List<ResultGetServicePhaseAllDto>> GetServicePhaseAllService
        {
            get
            {
                return _getServicePhaseAllService = _getServicePhaseAllService ?? new GetServicePhaseAllService(_context);
            }
        }

        private ICreateService<int?, RequestCreateServicePhaseDto> _createServicePhaseService;
        public ICreateService<int?, RequestCreateServicePhaseDto> CreateServicePhaseService
        {
            get
            {
                return _createServicePhaseService = _createServicePhaseService ?? new CreateServicePhaseService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateServicePhaseDto> _updateServicePhaseService;
        public IUpdateService<int?, RequestUpdateServicePhaseDto> UpdateServicePhaseService
        {
            get
            {
                return _updateServicePhaseService = _updateServicePhaseService ?? new UpdateServicePhaseService(_context);
            }
        }

        private IDeleteService _deleteServicePhaseService;
        public IDeleteService DeleteServicePhaseService
        {
            get
            {
                return _deleteServicePhaseService = _deleteServicePhaseService ?? new DeleteServicePhaseService(_context);
            }
        }
    }
}
