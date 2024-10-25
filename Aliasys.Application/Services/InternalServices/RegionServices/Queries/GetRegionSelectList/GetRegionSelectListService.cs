using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.RegionServices.Queries.GetRegionSelectList
{
    public class GetRegionSelectListService : IGetService<List<SelectListItem>>
    {
        private readonly IDataBaseContext _context;
        public GetRegionSelectListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SelectListItem>> Get()
        {
            try
            {
                var regionList = _context.Regions.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.CountryName + "/" + p.CapitalName,
                }).AsNoTracking().OrderBy(p => Convert.ToInt32(p.Value)).ToList();
                if (regionList != null)
                {
                    return new ResultDto<List<SelectListItem>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = regionList
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
