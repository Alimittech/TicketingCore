using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.IServices.InternalServices;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Commands.CreateUserInGroup;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Commands.DeleteUserInGroup;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.FindUserInGroup;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupFullList.GetUserInGroupWithGrpName;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupSelectList;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupWithUserId;
using Aliasys.Domain.Entities.UserEntities;
using Aliasys.Persistence.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO.Pipes;

namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.FacadPatterns
{
    public class UserInGroupFacad : IUserInGroupFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IRepository<UserInGroup> _repository;
        public UserInGroupFacad(IDataBaseContext context, IRepository<UserInGroup> repository)
        {
            _context = context;
            _repository = repository;
        }

        private IFindService<ResultFindUserInGroupDto, int> _findUserInGroupWithIdService;
        public IFindService<ResultFindUserInGroupDto, int> FindUserInGroupWithIdService
        {
            get
            {
                return _findUserInGroupWithIdService = _findUserInGroupWithIdService ?? new FindUserInGroupWithIdService(_context);
            }
        }

        private IFindService<List<ResultFindUserInGroupDto>, int> _findUserInGroupWithUserIdService;
        public IFindService<List<ResultFindUserInGroupDto>, int> FindUserInGroupWithUserIdService
        {
            get
            {
                return _findUserInGroupWithUserIdService = _findUserInGroupWithUserIdService ?? new FindUserInGroupWithUserIdService(_context);
            }
        }

        private IGetService<List<SelectListItem>, int> _getUserInGroupSelectListService;
        public IGetService<List<SelectListItem>, int> GetUserInGroupSelectListService
        {
            get
            {
                return _getUserInGroupSelectListService = _getUserInGroupSelectListService ?? new GetUserInGroupSelectListService(_context);
            }
        }

        private IGetService<List<SelectListItem>, string> _getUserInGroupSelectListWithGrpNameService;
        public IGetService<List<SelectListItem>, string> GetUserInGroupSelectListWithGrpNameService
        {
            get
            {
                return _getUserInGroupSelectListWithGrpNameService = _getUserInGroupSelectListWithGrpNameService ?? new GetUserInGroupSelectListWithGrpNameService(_context);
            }
        }

        private ICreateService<int?, RequestCreateUserInGroupDto> _createUserInGroupService;
        public ICreateService<int?, RequestCreateUserInGroupDto> CreateUserInGroupService
        {
            get
            {
                return _createUserInGroupService = _createUserInGroupService ?? new CreateUserInGroupService(_context);
            }
        }

        private IGetService<List<int>, int> _getUserInGroupWithUserIdService;
        public IGetService<List<int>, int> GetUserInGroupWithUserIdService
        {
            get
            {
                return _getUserInGroupWithUserIdService = _getUserInGroupWithUserIdService ?? new GetUserInGroupWithUserIdService(_context);
            }
        }

        private IDeleteService _deleteUserInGroupService;
        public IDeleteService DeleteUserInGroupService
        {
            get
            {
                return _deleteUserInGroupService = _deleteUserInGroupService ?? new DeleteUserInGroupService(_context);
            }
        }

        private IUserInGroupService _userInGroupService;

        public IUserInGroupService UserInGroupService
        {
            get
            {
                return _userInGroupService = _userInGroupService ?? new FindUserInGroupCustomService(_repository);
            }
        }
        
    }
}
