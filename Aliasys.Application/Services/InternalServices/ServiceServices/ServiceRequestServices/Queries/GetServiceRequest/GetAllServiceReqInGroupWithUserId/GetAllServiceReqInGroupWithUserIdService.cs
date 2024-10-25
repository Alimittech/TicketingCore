using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqInGroupWithUserId
{
    public class GetAllServiceReqInGroupWithUserIdService : IGetService<ResultGetAllServiceReqInGroupDto, RequestGetAllServiceReqInGroupDto>
    {
        private readonly IDataBaseContext _context;
        public GetAllServiceReqInGroupWithUserIdService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetAllServiceReqInGroupDto> Get(RequestDto<RequestGetAllServiceReqInGroupDto> request)
        {
            try
            {
                var groups = _context.UserInGroups.AsNoTracking().Where(x => x.UserId_FK == request.Parameter.UserId).ToList();
                var cats = _context.ServiceCategories.AsNoTracking().ToList();
                var ReqTypes = _context.ServiceRequestTypes.AsNoTracking().ToList();

                var ReqLifeCycles = request.Parameter.ShowAllRequest ?
                    (request.Parameter.ShowSearchkey ?
                    _context.ServiceRequestLifeCycles.AsNoTracking().Where(p => p.Description == request.Parameter.Pagination.SearchKey).ToList() :
                    _context.ServiceRequestLifeCycles.AsNoTracking().Where(p => p.ProcessUserId == request.Parameter.UserId).GroupBy(p => p.ServiceRequestId_FK).Select(p => p.OrderBy(c => c.Id).LastOrDefault()).ToList()) :
                    _context.ServiceRequestLifeCycles.AsNoTracking().GroupBy(p => p.ServiceRequestId_FK).Select(p => p.OrderBy(c => c.Id).LastOrDefault()).ToList();


                    //_context.ServiceRequestLifeCycles.AsNoTracking().Where(p => p.ProcessUserId == request.Parameter.UserId).GroupBy(p => p.ServiceRequestId_FK).Select(p => p.OrderBy(c => c.Id).LastOrDefault()).ToList() :
                    //_context.ServiceRequestLifeCycles.AsNoTracking().GroupBy(p => p.ServiceRequestId_FK).Select(p => p.OrderBy(c => c.Id).LastOrDefault()).ToList();

                //var SearchKey = _context.ServiceRequests.AsNoTracking().Where(p => p.Title == request.Parameter.Pagination.SearchKey).ToList();
                

                //var SeachKey2 = _context.ServiceRequestLifeCycles.AsNoTracking().Where(p => p.Description == request.Parameter.Pagination.SearchKey).ToList();


                var States = _context.ServiceStates.AsNoTracking().ToList();
                var Phases = _context.ServicePhases.AsNoTracking().ToList();
                var users = _context.Users.AsNoTracking().ToList();

                var phaseFilter = request.Parameter.ShowAllRequest ?
                _context.ServicePhases.AsNoTracking().Select(x => new { phaseId = x.Id, }).ToList() :
                _context.ServicePhases.AsNoTracking().Where(x => x.PhaseName != "Reject" && x.PhaseName != "Confirm").Select(x => new { phaseId = x.Id, }).ToList();

                var userCatList = cats.Join(groups, cat => cat.UserGroupId_FK, grp => grp.UserGroupId_FK,
                                    (cat, grp) => new
                                    {
                                        catId = cat.Id,
                                        catName = cat.Name
                                    });
                var reqs = _context.ServiceRequests.AsNoTracking().ToList();
                var result = reqs.Join(userCatList, req => req.ServiceCategoryId_FK, cat => cat.catId,
                                    (req, cat) => (req, cat))
                                        .Join(ReqTypes, req => req.req.ServiceRequestTypeId_FK, srt => srt.Id,
                                            (req, srt) => (req, srt))
                                        .Join(ReqLifeCycles, req => req.req.req.Id, rlc => rlc.ServiceRequestId_FK,
                                            (req, rlc) => (req, rlc))
                                        .Join(phaseFilter, req => req.rlc.ServicePhaseId_FK, phf => phf.phaseId,
                                            (req, phf) => (req, phf))
                                        .Join(States, req => req.req.rlc.ServiceStateId_FK, stt => stt.Id,
                                            (req, stt) => (req, stt))
                                        .Join(Phases, req => req.req.req.rlc.ServicePhaseId_FK, phs => phs.Id,
                                            (req, phs) => (req, phs))
                                        .Join(users, req => req.req.req.req.req.req.req.OwnerUserId_FK, usr => usr.Id,
                                            (req, usr) => (req, usr))
                                        .Join(users, req => req.req.req.req.req.rlc.ProcessUserId, usr1 => usr1.Id,
                                            (req, usr1) => (req, usr1))
                                        //.Join(SearchKey, req => req.req.req.req.req.req.req.req.req.Id ,Sk => Sk.Id,
                                        //    (req,Sk) => (req, Sk))
                                        .Join(users, req => req.req.req.req.req.req.rlc.AssignedUserId, usr2 => usr2.Id,
                                            (req, usr2) => new
                                            {
                                                finalRequestId = req.req.req.req.req.req.req.req.req.Id,
                                                finalUserId = req.req.req.req.req.req.req.req.req.OwnerUserId_FK,
                                                finalReqNumber = req.req.req.req.req.req.req.req.req.RequestNumber,
                                                finalCategory = req.req.req.req.req.req.req.req.cat.catName,
                                                finalReqType = req.req.req.req.req.req.req.srt.Name,
                                                finalRegisterDT = req.req.req.req.req.req.req.req.req.CreatedDateTime,
                                                finalStateName = req.req.req.req.stt.StateName,
                                                finalPhaseName = req.req.req.phs.PhaseName,
                                                finalOwnerName = req.req.usr.DisplayName,
                                                finalProcessorName = req.usr1.DisplayName,
                                                //finalSearchkey = req.usr1.DisplayName,
                                                finalAssignedUserName = usr2.DisplayName
                                            }).AsQueryable();

                int rowCount = 0;
                if (!string.IsNullOrWhiteSpace(request.Parameter.Pagination.SearchKey))
                {
                    result = result.Where(p =>  p.finalCategory.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            || p.finalReqNumber.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())

                                            || p.finalReqType.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            || p.finalStateName.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            || p.finalPhaseName.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            //|| p.finalSearchkey.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            ).AsQueryable() ;
                }
                var finalResult = result.ToPaged(request.Parameter.Pagination.Page, request.Parameter.Pagination.PageSize ,out rowCount)
                                                            .Select(p => new ResGetAllServiceReqInGroupDto
                                                            {
                                                                Id = p.finalRequestId,
                                                                UserId = p.finalUserId,
                                                                RequestNumber = p.finalReqNumber,
                                                                ServiceCategory = p.finalCategory,
                                                                ServiceRequestType = p.finalReqType,
                                                                RegisterDateTime = p.finalRegisterDT,
                                                                StateName = p.finalStateName,
                                                                PhaseName = p.finalPhaseName,
                                                                OwnerName = p.finalOwnerName,
                                                                ProcessorName = p.finalProcessorName,
                                                                //searchKey =p.finalSearchkey,
                                                            }).ToList();
                if (finalResult.Any())
                {
                    return new ResultDto<ResultGetAllServiceReqInGroupDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetAllServiceReqInGroupDto
                        {
                            srvRequestList = finalResult,
                            RowsCount = rowCount,
                            Page = request.Parameter.Pagination.Page,
                            PageSize = request.Parameter.Pagination.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetAllServiceReqInGroupDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no item",
                    Data = new ResultGetAllServiceReqInGroupDto
                    {
                        srvRequestList = null,
                        RowsCount = rowCount,
                        Page = request.Parameter.Pagination.Page,
                        PageSize = request.Parameter.Pagination.PageSize
                    }
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetAllServiceReqInGroupDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetAllServiceReqInGroupDto>
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
                    return new ResultDto<ResultGetAllServiceReqInGroupDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetAllServiceReqInGroupDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
