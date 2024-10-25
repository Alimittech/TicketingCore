using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgFullList
{
    public class GetOrganizationFullListService : IGetService<ResultGetOrganizationFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetOrganizationFullListService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetOrganizationFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var organizations = _context.Organizations.AsNoTracking().ToList();
                var regions = _context.Regions.AsNoTracking().ToList();
                int rowCount = 0;
                if (organizations.Any() && regions.Any())
                {
                    var orgList = organizations.Join(organizations, organ => organ.ParentId_FK, childOrg => childOrg.Id,
                                        (organ, childOrg) => new
                                        {
                                            OrgId = organ.Id,
                                            OrgName = organ.Name,
                                            OrgDomainName = organ.DomainName,
                                            OrgAddress = organ.Address,
                                            OrgPhone = organ.Phone,
                                            OrgParent = childOrg.Name,
                                            OrgRegion = organ.RegionId_FK,
                                        })
                                    .Join(regions, org => org.OrgRegion, reg => reg.Id,
                                        (org, reg) => new
                                        {
                                            orgId = org.OrgId,
                                            orgName = org.OrgName,
                                            orgDomainName = org.OrgDomainName,
                                            orgAddress = org.OrgAddress,
                                            orgPhone = org.OrgPhone,
                                            orgpParent = org.OrgParent,
                                            regName = reg.CountryName + "/" + reg.CapitalName
                                        }).AsQueryable();
                    if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                    {
                        orgList = orgList.Where(p => p.orgName.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                                  || p.orgAddress.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                                  || p.orgPhone.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                                  || p.orgDomainName.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                    }
                    var finalOrgList = orgList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount).Select(p => new RequestGetOrganizationFullListDto
                    {
                        Id = p.orgId,
                        ParentOrganization = p.orgpParent,
                        Name = p.orgName,
                        DomainName = p.orgDomainName,
                        RegionName = p.regName,
                        Address = p.orgAddress,
                        Phone = p.orgPhone,
                        //RegionName = p.Regions.First(s => s.Id == p.RegionId_FK).CountryName.ToString(),
                    }).ToList();
                    return new ResultDto<ResultGetOrganizationFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetOrganizationFullListDto
                        {
                            orgList = finalOrgList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetOrganizationFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no items",
                    Data = new ResultGetOrganizationFullListDto
                    {
                        orgList = null,
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
                    return new ResultDto<ResultGetOrganizationFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetOrganizationFullListDto>
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
                    return new ResultDto<ResultGetOrganizationFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetOrganizationFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
