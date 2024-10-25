using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.GetServiceRequestType.GetServiceReqTypeFullList
{
    public class GetServiceRequestTypeFullListService : IGetService<ResultGetServiceReqTypeFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetServiceRequestTypeFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetServiceReqTypeFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var srvReqTypeList = _context.ServiceRequestTypes.AsNoTracking().AsQueryable();
                int rowCount = 0;
                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    srvReqTypeList = srvReqTypeList.Where(p => p.Name.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalSrvReqTypeList = srvReqTypeList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount)
                                                        .Select(p => new RequestGetServiceReqTypeFullListDto
                                                        {
                                                            Id = p.Id,
                                                            RequestType = p.RequestType,
                                                            Name = p.Name,
                                                            BriefName = p.BriefName,
                                                        }).ToList();
                if (finalSrvReqTypeList.Any())
                {
                    return new ResultDto<ResultGetServiceReqTypeFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetServiceReqTypeFullListDto
                        {
                            srvReqTypeList = finalSrvReqTypeList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetServiceReqTypeFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no item",
                    Data = new ResultGetServiceReqTypeFullListDto
                    {
                        srvReqTypeList = null,
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
                    return new ResultDto<ResultGetServiceReqTypeFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceReqTypeFullListDto>
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
                    return new ResultDto<ResultGetServiceReqTypeFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceReqTypeFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
