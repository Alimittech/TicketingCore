using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.GetServiceCategory.GetServiceCatFullList
{
    public class GetServiceCategoryFullListService : IGetService<ResultGetServiceCategoryFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetServiceCategoryFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetServiceCategoryFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var srvCategories = _context.ServiceCategories.AsNoTracking().ToList();
                var userGroups = _context.UserGroups.AsNoTracking().ToList();
                var srvCategoryList = srvCategories.Join(userGroups, srvCat => srvCat.UserGroupId_FK, userGrp => userGrp.Id,
                                                    (srvCat, userGrp) => new
                                                    {
                                                        finalSrvCatId = srvCat.Id,
                                                        finalSrvCatName = srvCat.Name,
                                                        finalSrvCatUserGroup = userGrp.GroupName
                                                    }).AsQueryable();
                int rowCount = 0;

                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    srvCategoryList = srvCategoryList.Where(p => p.finalSrvCatName.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalSrvCategoryList = srvCategoryList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount)
                                                            .Select(p => new RequestGetServiceCategoryFullListDto
                                                            {
                                                                Id = p.finalSrvCatId,
                                                                Name = p.finalSrvCatName,
                                                                UserGroup = p.finalSrvCatUserGroup
                                                            }).ToList();
                if (finalSrvCategoryList.Any())
                {
                    return new ResultDto<ResultGetServiceCategoryFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetServiceCategoryFullListDto
                        {
                            srvCategoryList = finalSrvCategoryList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetServiceCategoryFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no item",
                    Data = new ResultGetServiceCategoryFullListDto
                    {
                        srvCategoryList = null,
                        RowsCount = rowCount,
                        Page = request.Parameter.Page,
                        PageSize = request.Parameter.PageSize
                    }
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetServiceCategoryFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceCategoryFullListDto>
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
                    return new ResultDto<ResultGetServiceCategoryFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceCategoryFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
