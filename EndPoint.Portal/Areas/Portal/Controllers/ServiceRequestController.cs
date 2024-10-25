using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Commands.CreateSrvReqLifeCycle;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Commands.CreateServiceRequest;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithManagerId;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithUserId;
using Aliasys.Common.Dtos;
using Aliasys.Common.ExtentionMethods;
using Aliasys.Common.Paginations;
using Aliasys.Domain.Entities.ServiceEntities;
using EndPoint.Portal.Areas.Portal.Models.ViewModels.RequestViewModel;
using EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequest;
using EndPoint.Portal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class ServiceRequestController : BaseController
    {
        private readonly IServiceRequestFacad _serviceRequestFacad;
        private readonly IUploadFileService _uploadFileService;
        private readonly IServiceRequestLifeCycleFacad _serviceReqLifeCycleFacad;
        private readonly IServiceStateFacad _serviceStateFacad;
        private readonly IServicePhaseFacad _servicePhaseFacad;
        private readonly IOperationUnitDependencyFacad _opunitDepFacad;
        public ServiceRequestController(IServiceRequestFacad serviceRequestFacad,
                                        IUploadFileService uploadFileService,
                                        IServiceRequestLifeCycleFacad serviceReqLifeCycleFacad,
                                        IServiceStateFacad serviceStateFacad,
                                        IServicePhaseFacad servicePhaseFacad,
                                        IOperationUnitDependencyFacad opunitDepFacad)
        {
            _serviceRequestFacad = serviceRequestFacad;
            _uploadFileService = uploadFileService;
            _serviceReqLifeCycleFacad = serviceReqLifeCycleFacad;
            _serviceStateFacad = serviceStateFacad;
            _servicePhaseFacad = servicePhaseFacad;
            _opunitDepFacad = opunitDepFacad;
        }

        //---------- Index Action ----------
        #region Index Action
        [HttpGet]
        public IActionResult Index(PaginationDto paginationDto)
        {
            ViewBag.searchKey = paginationDto.SearchKey;
            ViewBag.page = paginationDto.Page;
            ViewBag.pageSize = paginationDto.PageSize;
            return View();
        }
        #endregion

        //---------- List Action ----------
        #region List Action
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
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
                    string serviceReqListPath = "~/Areas/Portal/Views/ServiceRequest/Components/DataTableServiceRequest/DataTableServiceRequestViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, serviceReqListPath, result),
                        dataUpdate = "#DataTableServiceRequest"
                    });
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
                    string serviceReqListPath = "~/Areas/Portal/Views/ServiceRequest/Components/DataTableServiceRequest/DataTableServiceRequestViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, serviceReqListPath, result),
                        dataUpdate = "#DataTableServiceRequest"
                    });
                }
            }

            return Json(new { isValid = false, errorMessage = true });
        }
        #endregion

        //---------- Create Action ----------
        #region Create Action
        [HttpGet]
        public IActionResult Create(string httpVerb)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("CreateServiceRequest");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateServiceRequestDto request)
        {
            if (ModelState.IsValid)
            {
                TempDataUserDto userInfo = JsonConvert.DeserializeObject<TempDataUserDto>(TempData["UserInfo"].ToString());
                TempData.Keep();
                var createServiceRequest = _serviceRequestFacad.CreateServiceRequestService.Create(new RequestDto<RequestCreateServiceRequestDto>
                {
                    Parameter = new RequestCreateServiceRequestDto
                    {
                        RequestNumber = request.RequestNumber,
                        OwnerUserId = request.OwnerUserId,
                        ServiceCategoryId = request.ServiceCategoryId,
                        ServiceRequestTypeId = request.ServiceRequestTypeId,
                        ServicePriority = request.ServicePriority,
                        OccurDateTime = request.OccurDateTime,
                        ServiceAffected = request.ServiceAffected,
                        ImpactOn = request.ImpactOn,
                        Title = request.Title,
                        Description = request.Description,
                        FileAttachment = request.FileAttachment,
                    }
                });
                if (createServiceRequest.IsSuccess)
                {
                    var getServiceState = _serviceStateFacad.FindServiceStateWithNameService.Find(new RequestDto<string>
                    {
                        Parameter = request.ServiceState == "Create" ? "Running" : "Draft"
                    });
                    var getServicePhase = _servicePhaseFacad.FindServicePhaseWithNameService.Find(new RequestDto<string>
                    {
                        Parameter = request.ServiceState == "Create" ? "Handle" : "Creation"
                    });
                    //upload attachment file
                    string filename = null;
                    if (request.FileAttachment != null)
                    {
                        var upload = _uploadFileService.UploadFiles(request.FileAttachment);
                        if (upload.IsSuccess)
                        {
                            filename = upload.Data;
                        }
                    }

                    var createLifeCycle = _serviceReqLifeCycleFacad.CreateServiceReqLifeCycleService.Create(new RequestDto<RequestCreateSrvReqLifeCycleDto>
                    {
                        Parameter = new RequestCreateSrvReqLifeCycleDto
                        {
                            ServiceRequestId = createServiceRequest.Data,
                            ServiceStateId = getServiceState.Data.Id,
                            ServicePhaseId = getServicePhase.Data.Id,
                            ProcessAction = ProcessAction.None,
                            Description = request.Description,
                            AttachmentFileName = filename,
                            ProcessUserId = userInfo.UserId,
                            AssignedUserId = userInfo.UserId,
                        }
                    });
                    if (createLifeCycle.IsSuccess)
                    {
                        var getServiceReq = _serviceRequestFacad.GetAllServiceReqWithUserIdService.Get(new RequestDto<RequestAllGetSrvReqWithUserIdDto>
                        {
                            Parameter = new RequestAllGetSrvReqWithUserIdDto
                            {
                                Pagination = new PaginationDto
                                {
                                    Page = 1,
                                    PageSize = 5,
                                    SearchKey = string.Empty
                                },
                                UserId = userInfo.UserId
                            }
                        });
                        if (getServiceReq.ActionType == ActionType.Completed || getServiceReq.ActionType == ActionType.Null)
                        {
                            string serviceReqListPath = "~/Areas/Portal/Views/ServiceRequest/Components/DataTableServiceRequest/DataTableServiceRequestViewComponent.cshtml";
                            return Json(new
                            {
                                isValid = true,
                                html = Helper.RenderRazorViewToString(this, serviceReqListPath, getServiceReq.Data),
                                message = createServiceRequest.Message,
                                dataUpdate = "#DataTableServiceRequest"
                            });
                        }
                    }
                }
                return Json(new { isValid = false, message = createServiceRequest.Message, errorMessage = true });
            }
            string serviceReqCreatePath = "~/Areas/Portal/Views/ServiceRequest/Components/CreateServiceRequest/CreateServiceRequestViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceReqCreatePath, request) });
        }
        #endregion

        //---------- Change Action ----------
        #region Change Action
        [HttpGet]
        public IActionResult Change(string httpVerb, long Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("ChangeServiceRequest", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Change(RequestChangeRequest request)
        {
            if (ModelState.IsValid)
            {
                TempDataUserDto userInfo = JsonConvert.DeserializeObject<TempDataUserDto>(TempData["UserInfo"].ToString());
                TempData.Keep();
                int StateId = 0, PhaseId = 0;
                string stateOperationTypeName = ((StateOperationType)request.StateOperationType).ToString();
                var getAllState = _serviceStateFacad.GetServiceStateAllService.Get();
                var getAllPhase = _servicePhaseFacad.GetServicePhaseAllService.Get();
                if (getAllState.IsSuccess)
                {
                    if (getAllPhase.IsSuccess)
                    {
                        switch (stateOperationTypeName)
                        {
                            case "Cancel":
                                StateId = getAllState.Data.FirstOrDefault(x => x.StateName == "Cancelled").Id;
                                PhaseId = getAllPhase.Data.FirstOrDefault(x => x.PhaseName == "Reject").Id;
                                break;

                            case "Confirm":
                                StateId = getAllState.Data.FirstOrDefault(x => x.StateName == "Closed").Id;
                                PhaseId = getAllPhase.Data.FirstOrDefault(x => x.PhaseName == "Confirm").Id;
                                break;

                            case "Resubmit":
                                StateId = getAllState.Data.FirstOrDefault(x => x.StateName == "Running").Id;
                                PhaseId = getAllPhase.Data.FirstOrDefault(x => x.PhaseName == "Handle").Id;
                                break;
                        }
                        var createLifeCycle = _serviceReqLifeCycleFacad.CreateServiceReqLifeCycleService.Create(new RequestDto<RequestCreateSrvReqLifeCycleDto>
                        {
                            Parameter = new RequestCreateSrvReqLifeCycleDto
                            {
                                ServiceRequestId = request.ServiceRequestId,
                                ServiceStateId = StateId,
                                ServicePhaseId = PhaseId,
                                Description = request.Description,
                                AttachmentFileName = request.AttachmentFileName,
                                ProcessUserId = request.ProcessUserId,
                                AssignedUserId = request.AssignedUserId
                            }
                        });
                        if (createLifeCycle.IsSuccess)
                        {
                            var getServiceReq = _serviceRequestFacad.GetAllServiceReqWithUserIdService.Get(new RequestDto<RequestAllGetSrvReqWithUserIdDto>
                            {
                                Parameter = new RequestAllGetSrvReqWithUserIdDto
                                {
                                    Pagination = new PaginationDto
                                    {
                                        Page = 1,
                                        PageSize = 5,
                                        SearchKey = string.Empty
                                    },
                                    UserId = userInfo.UserId
                                }
                            });
                            if (getServiceReq.ActionType == ActionType.Completed || getServiceReq.ActionType == ActionType.Null)
                            {
                                string serviceReqListPath = "~/Areas/Portal/Views/ServiceRequest/Components/DataTableServiceRequest/DataTableServiceRequestViewComponent.cshtml";
                                return Json(new
                                {
                                    isValid = true,
                                    html = Helper.RenderRazorViewToString(this, serviceReqListPath, getServiceReq.Data),
                                    message = createLifeCycle.Message,
                                    dataUpdate = "#DataTableServiceRequest"
                                });
                            }
                        }
                        return Json(new { isValid = false, message = createLifeCycle.Message, errorMessage = true });
                    }
                }
            }
            string serviceRequestCreatePath = "~/Areas/Portal/Views/ServiceRequest/Components/CreateServiceRequest/CreateServiceRequestViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceRequestCreatePath, request) });
        }
        #endregion

        //---------- Update Action ----------
        #region Update Action

        #endregion

        //---------- Delete Action ----------
        #region Delete Action

        #endregion
    }
}
