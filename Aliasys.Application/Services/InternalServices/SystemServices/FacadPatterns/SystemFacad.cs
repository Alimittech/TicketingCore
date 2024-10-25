using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.CreateSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.DeleteSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.UpdateSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.FindSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemAll;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemFullList;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.SystemServices.FacadPatterns
{
    public class SystemFacad : ISystemFacad
    {
        private readonly IDataBaseContext _context;
        public SystemFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindSystemDto, int> _findSystemWithIdService;
        public IFindService<ResultFindSystemDto, int> FindSystemWithIdService
        {
            get
            {
                return _findSystemWithIdService = _findSystemWithIdService ?? new FindSystemWithIdService(_context);
            }
        }

        private IFindService<ResultFindSystemDto, string> _findSystemWithNameService;
        public IFindService<ResultFindSystemDto, string> FindSystemWithNameService
        {
            get
            {
                return _findSystemWithNameService = _findSystemWithNameService ?? new FindSystemWithNameService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getSystemSelectListService;
        public IGetService<List<SelectListItem>> GetSystemSelectListService
        {
            get
            {
                return _getSystemSelectListService = _getSystemSelectListService ?? new GetSystemSelectListService(_context);   
            }
        }

        private IGetService<ResultGetSystemFullListDto, PaginationDto> _getSystemFullListService;
        public IGetService<ResultGetSystemFullListDto, PaginationDto> GetSystemFullListService
        {
            get
            {
                return _getSystemFullListService = _getSystemFullListService ?? new GetSystemFullListService(_context);
            }
        }

        private IGetService<List<ResultGetSystemAllDto>> _getSystemAllService;
        public IGetService<List<ResultGetSystemAllDto>> GetSystemAllService
        {
            get
            {
                return _getSystemAllService = _getSystemAllService ?? new GetSystemAllService(_context);
            }
        }

        private ICreateService<int?, RequestCreateSystemDto> _createSystemService;
        public ICreateService<int?, RequestCreateSystemDto> CreateSystemService
        {
            get
            {
                return _createSystemService = _createSystemService ?? new CreateSystemService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateSystemDto> _updateSystemService;
        public IUpdateService<int?, RequestUpdateSystemDto> UpdateSystemService
        {
            get
            {
                return _updateSystemService = _updateSystemService ?? new UpdateSystemService(_context);
            }
        }

        private IDeleteService _deleteSystemService;
        public IDeleteService DeleteSystemService
        {
            get
            {
                return _deleteSystemService = _deleteSystemService ?? new DeleteSystemService(_context);
            }
        }
    }
}
