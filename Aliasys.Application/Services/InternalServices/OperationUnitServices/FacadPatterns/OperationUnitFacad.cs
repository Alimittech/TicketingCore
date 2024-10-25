using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.CreateOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.DeleteOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.UpdateOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.FindOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.GetOperationUnit.GetOpUnitFullList;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.GetOperationUnit.GetOpUnitSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.OperationUnitServices.FacadPatterns
{
    public class OperationUnitFacad : IOperationUnitFacad
    {
        private readonly IDataBaseContext _context;
        public OperationUnitFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindOperationUnitDto, int> _findOperationUnitWithIdService;
        public IFindService<ResultFindOperationUnitDto, int> FindOperationUnitWithIdService
        {
            get
            {
                return _findOperationUnitWithIdService = _findOperationUnitWithIdService ?? new FindOperationUnitWithIdService(_context);
            }
        }

        private IFindService<ResultFindOperationUnitDto, string> _findOperationUnitWithNameService;
        public IFindService<ResultFindOperationUnitDto, string> FindOperationUnitWithNameService
        {
            get
            {
                return _findOperationUnitWithNameService = _findOperationUnitWithNameService ?? new FindOperationUnitWithNameService(_context);
            }
        }

        private IFindService<ResultFindOperationUnitDto, int> _findOperationUnitWithCodeService;
        public IFindService<ResultFindOperationUnitDto, int> FindOperationUnitWithCodeService
        {
            get
            {
                return _findOperationUnitWithCodeService = _findOperationUnitWithCodeService ?? new FindOperationUnitWithCodeService(_context);
            }
        }

        private IGetService<ResultGetOperationUnitFullListDto, PaginationDto> _getOperationUnitFullListService;
        public IGetService<ResultGetOperationUnitFullListDto, PaginationDto> GetOperationUnitFullListService
        {
            get
            {
                return _getOperationUnitFullListService = _getOperationUnitFullListService ?? new GetOperationUnitFullListService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getOperationUnitSelectListService;
        public IGetService<List<SelectListItem>> GetOperationUnitSelectListService
        {
            get
            {
                return _getOperationUnitSelectListService = _getOperationUnitSelectListService ?? new GetOperationUnitSelectListService(_context);
            }
        }

        private ICreateService<int?, RequestCreateOperationUnitDto> _createOperationUnitService;
        public ICreateService<int?, RequestCreateOperationUnitDto> CreateOperationUnitService
        {
            get
            {
                return _createOperationUnitService = _createOperationUnitService ?? new CreateOperationUnitService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateOperationUnitDto> _updateOperationUnitService;
        public IUpdateService<int?, RequestUpdateOperationUnitDto> UpdateOperationUnitService
        {
            get
            {
                return _updateOperationUnitService = _updateOperationUnitService ?? new UpdateOperationUnitService(_context);
            }
        }

        private IDeleteService _deleteOperationUnitService;
        public IDeleteService DeleteOperationUnitService
        {
            get
            {
                return _deleteOperationUnitService = _deleteOperationUnitService ?? new DeleteOperationUnitService(_context);
            }
        }
    }
}
