using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupFullList.GetUserInGroupWithGrpName
{
    public class GetUserInGroupSelectListWithGrpNameService : IGetService<List<SelectListItem>, string>
    {
        private readonly IDataBaseContext _context;
        public GetUserInGroupSelectListWithGrpNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SelectListItem>> Get(RequestDto<string> request)
        {
            try
            {
                int getGroupId = _context.ServiceCategories.AsNoTracking().FirstOrDefault(p => p.Name == request.Parameter).UserGroupId_FK;
                var getUsersInGroup = _context.UserInGroups.AsNoTracking().Where(p => p.UserGroupId_FK == getGroupId).ToList();
                var users = _context.Users.AsNoTracking().ToList();
                var getUserInfo = getUsersInGroup.Join(users, uig => uig.UserId_FK, usr => usr.Id,
                                                                (uig, usr) => new SelectListItem
                                                                {
                                                                    Value = usr.Id.ToString(),
                                                                    Text = usr.DisplayName
                                                                }).OrderBy(p => Convert.ToInt32(p.Value)).ToList();
                if (getUserInfo != null)
                {
                    return new ResultDto<List<SelectListItem>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item(s) Found!",
                        Data = getUserInfo
                    };
                }
                return new ResultDto<List<SelectListItem>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "Item(s) Not Found!"
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<List<SelectListItem>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<SelectListItem>>
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
                    return new ResultDto<List<SelectListItem>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<SelectListItem>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
