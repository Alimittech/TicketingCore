using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithManagerId;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithUserId;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequest
{
    public class DataTableServiceRequestViewComponent : ViewComponent
    {
        private readonly IServiceRequestFacad _serviceRequestFacad;
        private readonly IOperationUnitDependencyFacad _opunitDepFacad;
        public DataTableServiceRequestViewComponent(IServiceRequestFacad serviceRequestFacad, IOperationUnitDependencyFacad opunitDepFacad)
        {
            _serviceRequestFacad = serviceRequestFacad;
            _opunitDepFacad = opunitDepFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page = 1, int pageSize = 10)
        {
            TempDataUserDto userInfo = JsonConvert.DeserializeObject<TempDataUserDto>(TempData["UserInfo"].ToString());
            TempData.Keep();

            ResultDto<ResultGetAllServiceReqDto> serviceReqList;
            serviceReqList = _serviceRequestFacad.GetAllServiceReqWithManagerIdService.Get(new RequestDto<RequestGetAllServiceReqInUnitDto>
            {
                Parameter = new RequestGetAllServiceReqInUnitDto
                {
                    Pagination = new PaginationDto
                    {
                        Page = page,
                        PageSize = pageSize,
                        SearchKey = searchKey
                    },
                    ManagerId = userInfo.UserId
                }
            });
            if (serviceReqList.IsSuccess)
            {
                if (serviceReqList.ActionType == ActionType.Completed || serviceReqList.ActionType == ActionType.Null)
                {
                    ResultGetAllServiceReqDto result = new ResultGetAllServiceReqDto
                    {
                        srvRequestList = serviceReqList.Data.srvRequestList ?? null,
                        Page = serviceReqList.Data.Page,
                        PageSize = serviceReqList.Data.PageSize,
                        RowsCount = serviceReqList.Data.RowsCount
                    };
                    return View(viewName: "DataTableServiceRequestViewComponent", result);
                }
            }
            else
            {
                serviceReqList = _serviceRequestFacad.GetAllServiceReqWithUserIdService.Get(new RequestDto<RequestAllGetSrvReqWithUserIdDto>
                {
                    Parameter = new RequestAllGetSrvReqWithUserIdDto
                    {
                        Pagination = new PaginationDto
                        {
                            Page = page,
                            PageSize = pageSize,
                            SearchKey = searchKey
                        },
                        UserId = userInfo.UserId
                    }
                });

                if (serviceReqList.ActionType == ActionType.Completed || serviceReqList.ActionType == ActionType.Null)
                {
                    ResultGetAllServiceReqDto result = new ResultGetAllServiceReqDto
                    {
                        srvRequestList = serviceReqList.Data.srvRequestList ?? null,
                        Page = serviceReqList.Data.Page,
                        PageSize = serviceReqList.Data.PageSize,
                        RowsCount = serviceReqList.Data.RowsCount
                    };
                    return View(viewName: "DataTableServiceRequestViewComponent", result);
                }
            }
            return View(viewName: "DataTableServiceRequestViewComponent");







            #region old code
            //var checkUserIsManager = _opunitDepFacad.FindOpunitDepWithManagerIdService.Find(userInfo.UserId);
            //if (checkUserIsManager.IsSuccess)
            //{
            //    var SrvReqList = _serviceRequestFacad.GetAllServiceReqWithManagerIdService.Get(new RequestDto<RequestGetAllServiceReqInUnitDto>
            //    {
            //        Parameter = new RequestGetAllServiceReqInUnitDto
            //        {
            //            Pagination = new PaginationDto
            //            {
            //                Page = page,
            //                PageSize = pageSize,
            //                SearchKey = searchKey,
            //            },
            //            ManagerId = userInfo.UserId
            //        }
            //    });

            //    ResultGetAllServiceReqDto res;
            //    res = new ResultGetAllServiceReqDto
            //    {
            //        srvRequestList = SrvReqList.Data.srvRequestList ?? null,
            //        Page = SrvReqList.Data.Page,
            //        PageSize = SrvReqList.Data.PageSize,
            //        RowsCount = SrvReqList.Data.RowsCount
            //    };
            //    return View(viewName: "DataTableServiceRequestViewComponent", res);
            //}

            //var serviceReqList = _serviceRequestFacad.GetAllServiceReqWithUserIdService.Get(new RequestDto<RequestAllGetSrvReqWithUserIdDto>
            //{
            //    Parameter = new RequestAllGetSrvReqWithUserIdDto
            //    {
            //        Pagination = new PaginationDto
            //        {
            //            Page = 1,
            //            PageSize = 5,
            //            SearchKey = string.Empty
            //        },
            //        UserId = userInfo.UserId
            //    }
            //});

            //ResultGetAllServiceReqDto result;
            //result = new ResultGetAllServiceReqDto
            //{
            //    srvRequestList = serviceReqList.Data.srvRequestList ?? null,
            //    Page = serviceReqList.Data.Page,
            //    PageSize = serviceReqList.Data.PageSize,
            //    RowsCount = serviceReqList.Data.RowsCount
            //};

            //return View(viewName: "DataTableServiceRequestViewComponent", result);
            #endregion








        }
    }
}
