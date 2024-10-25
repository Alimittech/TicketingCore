using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.CreateServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.DeleteServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.UpdateServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.FindServiceCategory;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.GetServiceCategory.GetServiceCatFullList;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.GetServiceCategory.GetServiceCatSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.FacadPatterns
{
    public class ServiceCategoryFacad : IServiceCategoryFacad
    {
        private readonly IDataBaseContext _context;
        public ServiceCategoryFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindServiceCategoryDto, int> _findServiceCategoryWithIdService;
        public IFindService<ResultFindServiceCategoryDto, int> FindServiceCategoryWithIdService
        {
            get
            {
                return _findServiceCategoryWithIdService = _findServiceCategoryWithIdService ?? new FindServiceCategoryWithIdService(_context);
            }
        }

        private IFindService<ResultFindServiceCategoryDto, string> _findServiceCategoryWithNameService;
        public IFindService<ResultFindServiceCategoryDto, string> FindServiceCategoryWithNameService
        {
            get
            {
                return _findServiceCategoryWithNameService = _findServiceCategoryWithNameService ?? new FindServiceCategoryWithNameService(_context);
            }
        }

        private IGetService<ResultGetServiceCategoryFullListDto, PaginationDto> _getServiceCategoryFullListService;
        public IGetService<ResultGetServiceCategoryFullListDto, PaginationDto> GetServiceCategoryFullListService
        {
            get
            {
                return _getServiceCategoryFullListService = _getServiceCategoryFullListService ?? new GetServiceCategoryFullListService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getServiceCategorySelectListService;
        public IGetService<List<SelectListItem>> GetServiceCategorySelectListService
        {
            get
            {
                return _getServiceCategorySelectListService = _getServiceCategorySelectListService ?? new GetServiceCategorySelectListService(_context);
            }
        }

        private ICreateService<int?, RequestCreateServiceCategoryDto> _createServiceCategoryService;
        public ICreateService<int?, RequestCreateServiceCategoryDto> CreateServiceCategoryService
        {
            get
            {
                return _createServiceCategoryService = _createServiceCategoryService ?? new CreateServiceCategoryService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateServiceCategoryDto> _updateServiceCategoryService;
        public IUpdateService<int?, RequestUpdateServiceCategoryDto> UpdateServiceCategoryService
        {
            get
            {
                return _updateServiceCategoryService = _updateServiceCategoryService ?? new UpdateServiceCategoryService(_context);
            }
        }

        private IDeleteService _deleteServiceCategoryService;
        public IDeleteService DeleteServiceCategoryService
        {
            get
            {
                return _deleteServiceCategoryService = _deleteServiceCategoryService ?? new DeleteServiceCategoryService(_context);
            }
        }
    }
}
