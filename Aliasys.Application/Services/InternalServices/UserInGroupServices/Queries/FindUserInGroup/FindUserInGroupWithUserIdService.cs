using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.FindUserInGroup
{
    public class FindUserInGroupWithUserIdService : IFindService<List<ResultFindUserInGroupDto>, int>
    {
        private readonly IDataBaseContext _context;
        public FindUserInGroupWithUserIdService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultFindUserInGroupDto>> Find(RequestDto<int> request)
        {
            try
            {
                var groups = _context.UserGroups.AsNoTracking().ToList();
                var userInGroups = _context.UserInGroups.AsNoTracking().Where(p => p.UserId_FK == request.Parameter).ToList();
                var result = userInGroups.Join(groups, uig => uig.UserGroupId_FK, grp => grp.Id,
                                                (uig,grp) => new ResultFindUserInGroupDto
                                                {
                                                    Id = uig.Id,
                                                    UserGroupId_FK = uig.UserGroupId_FK,
                                                    UserGroupName = grp.GroupName,
                                                    UserId_FK = uig.UserId_FK
                                                }).ToList();
                if (result != null)
                {
                    return new ResultDto<List<ResultFindUserInGroupDto>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = result
                    };
                }
                return new ResultDto<List<ResultFindUserInGroupDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                    Message = "No Record Found!"
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<List<ResultFindUserInGroupDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultFindUserInGroupDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "The application is not accessible!",
                };
            }
            catch (Exception ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<List<ResultFindUserInGroupDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultFindUserInGroupDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
