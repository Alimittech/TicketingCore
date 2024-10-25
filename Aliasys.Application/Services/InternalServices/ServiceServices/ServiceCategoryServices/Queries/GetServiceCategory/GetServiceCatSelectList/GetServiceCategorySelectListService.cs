using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Queries.GetServiceCategory.GetServiceCatSelectList
{
    public class GetServiceCategorySelectListService : IGetService<List<SelectListItem>>
    {
        private readonly IDataBaseContext _context;
        public GetServiceCategorySelectListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SelectListItem>> Get()
        {
            try
            {
                var serviceCatList = _context.ServiceCategories.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).AsNoTracking().OrderBy(p => Convert.ToInt32(p.Value)).ToList();
                if (serviceCatList.Any())
                {
                    return new ResultDto<List<SelectListItem>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = serviceCatList
                    };
                }
                return new ResultDto<List<SelectListItem>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null
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
