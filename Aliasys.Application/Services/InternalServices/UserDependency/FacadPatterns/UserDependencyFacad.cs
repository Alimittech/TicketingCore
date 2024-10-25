using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserDependency.Commands.CreateUserDependency;
using Aliasys.Application.Services.InternalServices.UserDependency.Commands.DeleteUserDependency;
using Aliasys.Application.Services.InternalServices.UserDependency.Queries.FindUserDependency;
using Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetOpunitManager;
using Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetUserDependency;

namespace Aliasys.Application.Services.InternalServices.UserDependency.FacadPatterns
{
    public class UserDependencyFacad : IUserDependencyFacad
    {
        private readonly IDataBaseContext _context;
        public UserDependencyFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindUserDependencyDto, int> _findUserDependencyWithIdService;
        public IFindService<ResultFindUserDependencyDto, int> FindUserDependencyWithIdService
        {
            get
            {
                return _findUserDependencyWithIdService = _findUserDependencyWithIdService ?? new FindUserDependencyWithIdService(_context);
            }
        }

        private IFindService<ResultFindUserDependencyDto, int> _findUserDependencyWithUserIdService;
        public IFindService<ResultFindUserDependencyDto, int> FindUserDependencyWithUserIdService
        {
            get
            {
                return _findUserDependencyWithUserIdService = _findUserDependencyWithUserIdService ?? new FindUserDependencyWithUserIdService(_context);
            }
        }

        private IGetService<ResultGetUserDepDto, int> _getUserDepWithUserIdService;
        public IGetService<ResultGetUserDepDto, int> GetUserDepWithUserIdService
        {
            get
            {
                return _getUserDepWithUserIdService = _getUserDepWithUserIdService ?? new GetUserDepWithUserIdService(_context);
            }
        }

        private IGetService<string, RequestUserDependencyDto> _getOpunitManagerService;
        public IGetService<string, RequestUserDependencyDto> GetOpunitManagerService
        {
            get
            {
                return _getOpunitManagerService = _getOpunitManagerService ?? new GetOpunitManagerService(_context);
            }
        }

        private ICreateService<int?, RequestCreateUserDependencyDto> _createUserDependencyService;
        public ICreateService<int?, RequestCreateUserDependencyDto> CreateUserDependencyService
        {
            get
            {
                return _createUserDependencyService = _createUserDependencyService ?? new CreateUserDependencyService(_context);
            }
        }

        private IDeleteService _deleteUserDependencyService;
        public IDeleteService DeleteUserDependencyService
        {
            get
            {
                return _deleteUserDependencyService = _deleteUserDependencyService ?? new DeleteUserDependencyService(_context);
            }
        }
    }
}
