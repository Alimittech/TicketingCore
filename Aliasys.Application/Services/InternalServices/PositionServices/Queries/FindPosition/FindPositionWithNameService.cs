using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.PositionServices.Queries.FindPosition
{
    public class FindPositionWithNameService : IFindService<ResultFindPositionDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindPositionWithNameService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindPositionDto> Find(RequestDto<string> request)
        {
            try
            {
                var result = _context.Positions.AsNoTracking().FirstOrDefault(p => p.Equals(request.Parameter));
                if (result != null)
                {
                    return new ResultDto<ResultFindPositionDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = new ResultFindPositionDto
                        {
                            Id = result.Id,
                            Name = result.Name,
                        }
                    };
                }
                return new ResultDto<ResultFindPositionDto>
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
                    return new ResultDto<ResultFindPositionDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindPositionDto>
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
                    return new ResultDto<ResultFindPositionDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindPositionDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
