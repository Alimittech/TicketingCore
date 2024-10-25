using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.UpdateOrganization
{
    public class UpdateOrganizationService : IUpdateService<int?, RequestUpdateOrganizationDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateOrganizationService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Update(RequestDto<RequestUpdateOrganizationDto> request)
        {
            try
            {
                var findOrg = _context.Organizations.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findOrg == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }

                var findOrgName = _context.Organizations.FirstOrDefault(p => p.Name == request.Parameter.Name
                                                                        && request.Parameter.Name != findOrg.Name);
                if (findOrgName != null)
                {
                    findOrgName = null;
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }

                var findOrgDomainName = _context.Organizations.FirstOrDefault(p => p.DomainName == request.Parameter.DomainName
                                                                        && request.Parameter.DomainName != findOrg.DomainName);
                if (findOrgDomainName != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This domain name is already registered!"
                    };
                }

                findOrg.Name = request.Parameter.Name;
                findOrg.DomainName = request.Parameter.DomainName;
                if (findOrg.Id != 1)
                {
                    findOrg.ParentId_FK = request.Parameter.ParentOrganization;
                }
                findOrg.RegionId_FK = request.Parameter.Region;
                findOrg.Phone = request.Parameter.Phone;
                findOrg.Address = request.Parameter.Address;
                findOrg.UpdatedDateTime = DateTime.Now;
                _context.Organizations.Update(findOrg);
                _context.SaveChanges();
                return new ResultDto<int?>
                {
                    IsSuccess = true,
                    ActionType = ActionType.Updated,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Update).Message
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<int?>
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
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<int?>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
