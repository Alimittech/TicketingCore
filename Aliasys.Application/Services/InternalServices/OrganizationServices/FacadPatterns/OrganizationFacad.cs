using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.CreateOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.DeleteOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.UpdateOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.FindOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgDependency;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgFullList;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.FacadPatterns
{
    public class OrganizationFacad : IOrganizationFacad
    {
        private readonly IDataBaseContext _context;
        public OrganizationFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindOrganizationDto, string> _findOrganizationWithNameService;
        public IFindService<ResultFindOrganizationDto, string> FindOrganizationWithNameService
        {
            get
            {
                return _findOrganizationWithNameService = _findOrganizationWithNameService ?? new FindOrganizationWithNameService(_context);
            }
        }

        private IFindService<ResultFindOrganizationDto, int> _findOrganizationWithIdService;
        public IFindService<ResultFindOrganizationDto, int> FindOrganizationWithIdService
        {
            get
            {
                return _findOrganizationWithIdService = _findOrganizationWithIdService ?? new FindOrganizationWithIdService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getOrganizationSelectListService;
        public IGetService<List<SelectListItem>> GetOrganizationSelectListService
        {
            get
            {
                return _getOrganizationSelectListService = _getOrganizationSelectListService ?? new GetOrganizationSelectListService(_context);
            }
        }

        private IGetService<ResultGetOrganizationFullListDto, PaginationDto> _getOrganizationFullListService;
        public IGetService<ResultGetOrganizationFullListDto, PaginationDto> GetOrganizationFullListService
        {
            get
            {
                return _getOrganizationFullListService = _getOrganizationFullListService ?? new GetOrganizationFullListService(_context);
            }
        }

        private IGetService<ResultGetOrganizationDependencyDto, int> _getOrganizationDependencyService;
        public IGetService<ResultGetOrganizationDependencyDto, int> GetOrganizationDependencyService
        {
            get
            {
                return _getOrganizationDependencyService = _getOrganizationDependencyService ?? new GetOrganizationDependencyService(_context);
            }
        }

        private ICreateService<int?, RequestCreateOrganizationDto> _createOrganizationService;
        public ICreateService<int?, RequestCreateOrganizationDto> CreateOrganizationService
        {
            get
            {
                return _createOrganizationService = _createOrganizationService ?? new CreateOrganizationService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateOrganizationDto> _updateOrganizationService;
        public IUpdateService<int?, RequestUpdateOrganizationDto> UpdateOrganizationService
        {
            get
            {
                return _updateOrganizationService = _updateOrganizationService ?? new UpdateOrganizationService(_context);
            }
        }

        private IDeleteService _deleteOrganizationService;
        public IDeleteService DeleteOrganizationService
        {
            get
            {
                return _deleteOrganizationService = _deleteOrganizationService ?? new DeleteOrganizationService(_context);
            }
        }
    }
}
