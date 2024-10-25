﻿using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceSubCauseServices.Queries.GetSubCause.GetActiveSubCauseSelectList
{
    public class GetServiceActiveSubCauseService : IGetService<List<CustomSelectListItem>>
    {
        private readonly IDataBaseContext _context;
        public GetServiceActiveSubCauseService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<CustomSelectListItem>> Get()
        {
            try
            {
                var subCauseList = _context.ServiceSubCauses.Where(p => !p.IsDeleted).Select(p => new CustomSelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.SubCauseName,
                    GroupId = p.ParentId
                }).AsNoTracking().OrderBy(p => Convert.ToInt32(p.Value)).ToList();
                if (subCauseList.Any())
                {
                    return new ResultDto<List<CustomSelectListItem>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = subCauseList
                    };
                }
                return new ResultDto<List<CustomSelectListItem>>
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
                    return new ResultDto<List<CustomSelectListItem>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<CustomSelectListItem>>
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
                    return new ResultDto<List<CustomSelectListItem>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<CustomSelectListItem>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
