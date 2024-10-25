using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.CreateUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.DeleteUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.UpdateUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Queries.FindUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Queries.GetUserGroup.GetUserGroupFullList;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Queries.GetUserGroup.GetUserGroupSelectList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.UserGroupServices.FacadPatterns
{
    public class UserGroupFacad : IUserGroupFacad
    {
        private readonly IDataBaseContext _context;
        public UserGroupFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IFindService<ResultFindUserGroupDto, int> _findUserGroupWithIdService;
        public IFindService<ResultFindUserGroupDto, int> FindUserGroupWithIdService
        {
            get
            {
                return _findUserGroupWithIdService = _findUserGroupWithIdService ?? new FindUserGroupWithIdService(_context);
            }
        }

        private IGetService<List<SelectListItem>> _getUserGroupSelectListService;
        public IGetService<List<SelectListItem>> GetUserGroupSelectListService
        {
            get
            {
                return _getUserGroupSelectListService = _getUserGroupSelectListService ?? new GetUserGroupSelectListService(_context);
            }
        }

        private IGetService<ResultGetUserGroupFullListDto, PaginationDto> _getUserGroupFullListService;
        public IGetService<ResultGetUserGroupFullListDto, PaginationDto> GetUserGroupFullListService
        {
            get
            {
                return _getUserGroupFullListService = _getUserGroupFullListService ?? new GetUserGroupFullListService(_context);
            }
        }

        private ICreateService<int?, RequestCreateUserGroupDto> _createUserGroupService;
        public ICreateService<int?, RequestCreateUserGroupDto> CreateUserGroupService
        {
            get
            {
                return _createUserGroupService = _createUserGroupService ?? new CreateUserGroupService(_context);
            }
        }

        private IUpdateService<int?, RequestUpdateUserGroupDto> _updateUserGroupService;
        public IUpdateService<int?, RequestUpdateUserGroupDto> UpdateUserGroupService
        {
            get
            {
                return _updateUserGroupService = _updateUserGroupService ?? new UpdateUserGroupService(_context);
            }
        }

        private IDeleteService _deleteUserGroupService;
        public IDeleteService DeleteUserGroupService
        {
            get
            {
                return _deleteUserGroupService = _deleteUserGroupService ?? new DeleteUserGroupService(_context);
            }
        }
    }
}
