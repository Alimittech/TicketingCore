using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.ChangeStateUser;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.CreateUser;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.DeleteUser;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.UpdateUser;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.FindUser;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserDetails;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserFullList;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserSelectList;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.ValidateLdapUser;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.UserServices.FacadPatterns
{
    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        public UserFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindDto, int> _findUserWithIdService;
        public IFindService<ResultFindDto, int> FindUserWithIdService
        {
            get
            {
                return _findUserWithIdService = _findUserWithIdService ?? new FindUserWithIdService(_context);
            }
        }

        private IFindService<ResultFindDto, string> _findUserWithUserNameService;
        public IFindService<ResultFindDto, string> FindUserWithUserNameService
        {
            get
            {
                return _findUserWithUserNameService = _findUserWithUserNameService ?? new FindUserWithUserNameService(_context);
            }
        }

        private IFindService<ResultFindDto, string> _findUserWithEmailService;
        public IFindService<ResultFindDto, string> FindUserWithEmailService
        {
            get
            {
                return _findUserWithEmailService = _findUserWithEmailService ?? new FindUserWithEmailService(_context);
            }
        }

        private IGetService<ResultGetUserFullListDto, PaginationDto> _getUserFullListService;
        public IGetService<ResultGetUserFullListDto, PaginationDto> GetUserFullListService
        {
            get
            {
                return _getUserFullListService = _getUserFullListService ?? new GetUserFullListService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getUserSelectListService;
        public IGetService<List<SelectListItem>> GetUserSelectListService
        {
            get
            {
                return _getUserSelectListService = _getUserSelectListService ?? new GetUserSelectListService(_context);
            }
        }

        private IGetService<ResultGetUserDetailWithUserNameDto, string> _getUserDetailWithUserNameService;
        public IGetService<ResultGetUserDetailWithUserNameDto, string> GetUserDetailWithUserNameService
        {
            get
            {
                return _getUserDetailWithUserNameService = _getUserDetailWithUserNameService ?? new GetUserDetailWithUserNameService(_context);
            }
        }

        private IValidateLdapUser<RequestValidateLdapUserDto> _validateLdapUser;
        public IValidateLdapUser<RequestValidateLdapUserDto> ValidateLdapUser
        {
            get
            {
                return _validateLdapUser = _validateLdapUser ?? new ValidateLdapUser();
            }
        }

        private ICreateService<int?, RequestCreateUserDto> _createUserService;
        public ICreateService<int?, RequestCreateUserDto> CreateUserService
        {
            get
            {
                return _createUserService = _createUserService ?? new CreateUserService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateUserDto> _updateUserService;
        public IUpdateService<int?, RequestUpdateUserDto> UpdateUserService
        {
            get
            {
                return _updateUserService = _updateUserService ?? new UpdateUserService(_context);
            }
        }

        private IDeleteService _deleteUserService;
        public IDeleteService DeleteUserService
        {
            get
            {
                return _deleteUserService = _deleteUserService ?? new DeleteUserService(_context);
            }
        }

        private IChangeStateUserService _changeStateUserService;
        public IChangeStateUserService ChangeStateUserService
        {
            get
            {
                return _changeStateUserService = _changeStateUserService ?? new ChangeStateUserService(_context);
            }
        }
    }
}
