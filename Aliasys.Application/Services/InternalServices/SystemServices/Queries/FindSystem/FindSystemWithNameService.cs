﻿using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.SystemServices.Queries.FindSystem
{
    public class FindSystemWithNameService : IFindService<ResultFindSystemDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindSystemWithNameService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindSystemDto> Find(RequestDto<string> request)
        {
            try
            {
                var result = (from s in _context.SystemComponents.AsNoTracking().Where(p => p.Name == request.Parameter)
                              join ps in _context.SystemComponents.AsNoTracking() on s.ParentSystemId equals ps.Id
                              orderby s.Id
                              select new
                              {
                                  sysId = s.Id,
                                  sysParentName = ps.Name,
                                  sysParentId = ps.Id,
                                  sysName = s.Name,
                                  sysDescription = s.Description,
                              }).FirstOrDefault();
                if (result != null)
                {
                    return new ResultDto<ResultFindSystemDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "The entered name is already exist!",
                        Data = new ResultFindSystemDto
                        {
                            Id = result.sysId,
                            ParentSystem = new SelectListItem { Value = result.sysParentId.ToString(), Text = result.sysParentName },
                            Name = result.sysName,
                            Description = result.sysDescription,
                        }
                    };
                }
                return new ResultDto<ResultFindSystemDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                    Message = "The enetered name is not exist!"
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultFindSystemDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindSystemDto>
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
                    return new ResultDto<ResultFindSystemDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindSystemDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
