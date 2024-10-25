using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.FindServiceCategory
{
    public class FindServiceCategoryWithIdService : IFindService<ResultFindServiceCategoryDto, int>
    {
        private readonly IDataBaseContext _context;
        public FindServiceCategoryWithIdService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultFindServiceCategoryDto> Find(RequestDto<int> request)
        {
            try
            {
                var result = _context.ServiceCategories.AsNoTracking().SingleOrDefault(p => p.Id == request.Parameter);
                if (result != null)
                {
                    return new ResultDto<ResultFindServiceCategoryDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = new ResultFindServiceCategoryDto
                        {
                            Id = result.Id,
                            Name = result.Name,
                            UserGroupId_FK = result.UserGroupId_FK,
                        }
                    };
                }
                return new ResultDto<ResultFindServiceCategoryDto>
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
                    return new ResultDto<ResultFindServiceCategoryDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceCategoryDto>
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
                    return new ResultDto<ResultFindServiceCategoryDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceCategoryDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
