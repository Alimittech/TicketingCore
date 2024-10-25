using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.FindOrganization
{
    public class FindOrganizationWithNameService : IFindService<ResultFindOrganizationDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindOrganizationWithNameService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindOrganizationDto> Find(RequestDto<string> request)
        {
            try
            {
                var Result = (from o in _context.Organizations.AsNoTracking().Where(p => p.Name == request.Parameter)
                              join po in _context.Organizations.AsNoTracking() on o.ParentId_FK equals po.Id
                              join r in _context.Regions.AsNoTracking() on o.RegionId_FK equals r.Id
                              orderby o.Id
                              select new
                              {
                                  orgId = o.Id,
                                  orgParentName = po.Name,
                                  orgParentId = po.Id,
                                  orgName = o.Name,
                                  orgDomainName = o.DomainName,
                                  orgRegionId = r.Id,
                                  orgRegionName = r.CountryName + "/" + r.CapitalName,
                                  orgAddress = o.Address,
                                  orgPhone = o.Phone
                              }).FirstOrDefault();
                if (Result != null)
                {
                    return new ResultDto<ResultFindOrganizationDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "The entered name is already exist!",
                        Data = new ResultFindOrganizationDto
                        {
                            Id = Result.orgId,
                            ParentOrganization = new SelectListItem { Value = Result.orgParentId.ToString(), Text = Result.orgParentName },
                            Name = Result.orgName,
                            DomainName = Result.orgDomainName,
                            Region = new SelectListItem { Value = Result.orgRegionId.ToString(), Text = Result.orgRegionName },
                            Address = Result.orgAddress,
                            Phone = Result.orgPhone,
                        }
                    };
                }
                return new ResultDto<ResultFindOrganizationDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                    Message = "The entered name is not exist!"
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultFindOrganizationDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindOrganizationDto>
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
                    return new ResultDto<ResultFindOrganizationDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindOrganizationDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
