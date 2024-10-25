using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.OrganizationEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.CreateOrganization
{
    public class CreateOrganizationService : ICreateService<int?, RequestCreateOrganizationDto>
    {
        private readonly IDataBaseContext _context;
        public CreateOrganizationService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Create(RequestDto<RequestCreateOrganizationDto> request)
        {
            try
            {
                var result = _context.Organizations.AsNoTracking().FirstOrDefault(p => p.Name.Trim().ToLower() == request.Parameter.Name.Trim().ToLower());
                if (result != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "The entered name is already exist!"
                    };
                }
                result = null;
                Organization newOrganization = new Organization
                {
                    Name = request.Parameter.Name.Trim(),
                    DomainName = request.Parameter.DomainName.Trim().ToLower(),
                    RegionId_FK = request.Parameter.Region,
                    Address = request.Parameter.Address.Trim(),
                    Phone = request.Parameter.Phone.Trim()
                };
                if (_context.Organizations.AsNoTracking().Any())
                {
                    newOrganization.ParentId_FK = request.Parameter.ParentOrganization;
                }
                else
                {
                    newOrganization.ParentId_FK = 1;
                }
                _context.Organizations.Add(newOrganization);
                _context.SaveChanges();
                return new ResultDto<int?>
                {
                    IsSuccess = true,
                    ActionType = ActionType.Created,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Create).Message
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
