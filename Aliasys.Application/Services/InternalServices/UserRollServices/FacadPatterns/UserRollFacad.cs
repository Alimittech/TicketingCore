using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.CreateUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.DeleteUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.UpdateUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Queries.FindUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Queries.GetUserRoll.GetUserRollAll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Queries.GetUserRoll.GetUserRollFullList;
using Aliasys.Common.Dtos;

namespace Aliasys.Application.Services.InternalServices.UserRollServices.FacadPatterns
{
    public class UserRollFacad : IUserRollFacad
    {
        private readonly IDataBaseContext _context;
        public UserRollFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindUserRollDto, int> _findUserRollWithIdService;
        public IFindService<ResultFindUserRollDto, int> FindUserRollWithIdService
        {
            get
            {
                return _findUserRollWithIdService = _findUserRollWithIdService ?? new FindUserRollWithIdService(_context);
            }
        }

        private IFindService<ResultFindUserRollDto, string> _findUserRollWithNameService;
        public IFindService<ResultFindUserRollDto, string> FindUserRollWithNameService
        {
            get
            {
                return _findUserRollWithNameService = _findUserRollWithNameService ?? new FindUserRollWithNameService(_context);
            }
        }

        private IGetService<ResultGetUserRollFullListDto, PaginationDto> _getUserRollFullListService;
        public IGetService<ResultGetUserRollFullListDto, PaginationDto> GetUserRollFullListService
        {
            get
            {
                return _getUserRollFullListService = _getUserRollFullListService ?? new GetUserRollFullListService(_context);
            }
        }

        private IGetService<List<ResultGetUserRollAllDto>> _getUserRollAllService;
        public IGetService<List<ResultGetUserRollAllDto>> GetUserRollAllService
        {
            get
            {
                return _getUserRollAllService = _getUserRollAllService ?? new GetUserRollAllService(_context);
            }
        }

        private ICreateService<int?, RequestCreateUserRollDto> _createUserRollService;
        public ICreateService<int?, RequestCreateUserRollDto> CreateUserRollService
        {
            get
            {
                return _createUserRollService = _createUserRollService ?? new CreateUserRollService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateUserRollDto> _updateUserRollService;
        public IUpdateService<int?, RequestUpdateUserRollDto> UpdateUserRollService
        {
            get
            {
                return _updateUserRollService = _updateUserRollService ?? new UpdateUserRollService(_context);  
            }
        }

        private IDeleteService _deleteUserRollService;
        public IDeleteService DeleteUserRollService
        {
            get
            {
                return _deleteUserRollService = _deleteUserRollService ?? new DeleteUserRollService(_context);
            }
        }
    }
}
