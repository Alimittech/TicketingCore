using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.CreatePosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.DeletePosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.UpdatePosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Queries.FindPosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Queries.GetPosition.GetPositionFullList;
using Aliasys.Application.Services.InternalServices.PositionServices.Queries.GetPosition.GetPositionSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.PositionServices.FacadPatterns
{
    public class PositionFacad : IPositionFacad
    {
        private readonly IDataBaseContext _context;
        public PositionFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindPositionDto, int> _findPositionWithIdService;
        public IFindService<ResultFindPositionDto, int> FindPositionWithIdService
        {
            get
            {
                return _findPositionWithIdService = _findPositionWithIdService ?? new FindPositionWithIdService(_context);
            }
        }

        private IFindService<ResultFindPositionDto, string> _findPositionWithNameService;
        public IFindService<ResultFindPositionDto, string> FindPositionWithNameService
        {
            get
            {
                return _findPositionWithNameService = _findPositionWithNameService ?? new FindPositionWithNameService(_context);
            }
        }

        private IGetService<ResultGetPositionFullListDto, PaginationDto> _getPositionFullListService;
        public IGetService<ResultGetPositionFullListDto, PaginationDto> GetPositionFullListService
        {
            get
            {
                return _getPositionFullListService = _getPositionFullListService ?? new GetPositionFullListService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getPositionSelectListService;
        public IGetService<List<SelectListItem>> GetPositionSelectListService
        {
            get
            {
                return _getPositionSelectListService = _getPositionSelectListService ?? new GetPositionSelectListService(_context); 
            }
        }

        private ICreateService<int?, RequestCreatePositionDto> _createPositionService;
        public ICreateService<int?, RequestCreatePositionDto> CreatePositionService
        {
            get
            {
                return _createPositionService = _createPositionService ?? new CreatePositionService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdatePositionDto> _updatePositionService;
        public IUpdateService<int?, RequestUpdatePositionDto> UpdatePositionService
        {
            get
            {
                return _updatePositionService = _updatePositionService ?? new UpdatePositionService(_context);
            }
        }

        private IDeleteService _deletePositionService;
        public IDeleteService DeletePositionService
        {
            get
            {
                return _deletePositionService = _deletePositionService ?? new DeletePositionService(_context);
            }
        }
    }
}
