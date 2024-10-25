using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.CreateServiceRequestType;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.UpdateServiceRequestType;
using Aliasys.Common.Dtos;
using Aliasys.Common.ExtentionMethods;
using Aliasys.Common.Paginations;
using EndPoint.Portal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class ServiceRequestTypeController : BaseController
    {
        private readonly IServiceRequestTypeFacad _serviceRequestTypeFacad;
        public ServiceRequestTypeController(IServiceRequestTypeFacad serviceRequestTypeFacad)
        {
            _serviceRequestTypeFacad = serviceRequestTypeFacad;
        }

        //---------- List Action ----------
        #region List Action
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getServiceReqType = _serviceRequestTypeFacad.GetServiceRequestTypeFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getServiceReqType.ActionType == ActionType.Completed || getServiceReqType.ActionType == ActionType.Null)
            {
                string serviceReqTypeListPath = "~/Areas/Portal/Views/ServiceRequestType/Components/DataTableServiceRequestType/DataTableServiceRequestTypeViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, serviceReqTypeListPath, getServiceReqType.Data),
                    message = getServiceReqType.Message,
                    dataUpdate = "#DataTableServiceRequestType"
                });
            }
            return Json(new { isValid = false, message = getServiceReqType.Message, errorMessage = true });
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
                    return ViewComponent("CreateServiceRequestType");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateServiceRequestTypeDto request)
        {
            if (ModelState.IsValid)
            {
                var createServiceReqType = _serviceRequestTypeFacad.CreateServiceRequestTypeService.Create(new RequestDto<RequestCreateServiceRequestTypeDto>
                {
                    Parameter = new RequestCreateServiceRequestTypeDto
                    {
                        RequestType = request.RequestType,
                        Name = request.Name,
                        BriefName = request.BriefName,
                    }
                });
                if (createServiceReqType.IsSuccess)
                {
                    var getServiceReqType = _serviceRequestTypeFacad.GetServiceRequestTypeFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServiceReqType.ActionType == ActionType.Completed || getServiceReqType.ActionType == ActionType.Null)
                    {
                        string serviceReqTypeListPath = "~/Areas/Portal/Views/ServiceRequestType/Components/DataTableServiceRequestType/DataTableServiceRequestTypeViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, serviceReqTypeListPath, getServiceReqType.Data),
                            message = createServiceReqType.Message,
                            dataUpdate = "#DataTableServiceRequestType"
                        });
                    }
                }
                return Json(new { isValid = false, message = createServiceReqType.Message, errorMessage = true });
            }
            string serviceReqTypeCreatePath = "~/Areas/Portal/Views/ServiceRequestType/Components/CreateServiceRequestType/CreateServiceRequestTypeViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceReqTypeCreatePath, request) });
        }
        #endregion

        //---------- Update Action ----------
        #region Update Action
        [HttpGet]
        public IActionResult Update(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    var serviceReqTypeFind = _serviceRequestTypeFacad.FindServiceRequestTypeWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (serviceReqTypeFind.IsSuccess)
                    {
                        var model = new RequestUpdateServiceRequestTypeDto
                        {
                            Id = serviceReqTypeFind.Data.Id,
                            RequestType = serviceReqTypeFind.Data.RequestType,
                            Name = serviceReqTypeFind.Data.Name,
                            BriefName = serviceReqTypeFind.Data.BriefName
                        };
                        return ViewComponent("UpdateServiceRequestType", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = serviceReqTypeFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateServiceRequestTypeDto request)
        {
            if (ModelState.IsValid)
            {
                var updateServiceReqType = _serviceRequestTypeFacad.UpdateServiceRequestTypeService.Update(new RequestDto<RequestUpdateServiceRequestTypeDto>
                {
                    Parameter = request
                });
                if (updateServiceReqType.IsSuccess)
                {
                    var getServiceReqType = _serviceRequestTypeFacad.GetServiceRequestTypeFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServiceReqType.ActionType == ActionType.Completed || getServiceReqType.ActionType == ActionType.Null)
                    {
                        string serviceReqTypeListPath = "~/Areas/Portal/Views/ServiceRequestType/Components/DataTableServiceRequestType/DataTableServiceRequestTypeViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, serviceReqTypeListPath, getServiceReqType.Data),
                            message = updateServiceReqType.Message,
                            dataUpdate = "#DataTableServiceRequestType"
                        });
                    }
                    return Json(new { isValid = false, message = getServiceReqType.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateServiceReqType.Message, errorMessage = true });
            }
            string serviceReqTypeUpdatePath = "~/Areas/Portal/Views/ServiceRequestType/Components/DataTableServiceRequestType/DataTableServiceRequestTypeViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceReqTypeUpdatePath, request) });
        }
        #endregion

        //---------- Delete Action ----------
        #region Delete Action
        [HttpGet]
        public IActionResult Delete(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    ViewBag.RouteAction = "deleteServiceRequestType";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var serviceReqType = _serviceRequestTypeFacad.DeleteServiceRequestTypeService.Delete(Id);
            if (serviceReqType.IsSuccess)
            {
                var getServiceReqType = _serviceRequestTypeFacad.GetServiceRequestTypeFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getServiceReqType.ActionType == ActionType.Completed || getServiceReqType.ActionType == ActionType.Null)
                {
                    string serviceReqTypeListPath = "~/Areas/Portal/Views/ServiceRequestType/Components/DataTableServiceRequestType/DataTableServiceRequestTypeViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, serviceReqTypeListPath, getServiceReqType.Data),
                        message = serviceReqType.Message,
                        dataUpdate = "#DataTableServiceRequestType"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getServiceReqType.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = serviceReqType.Message });
        }
        #endregion

        //---------- Check Action ----------
        #region Check Action
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckServiceReqTypeName(string Name)
        {
            var result = _serviceRequestTypeFacad.FindServiceRequestTypeWithNameService.Find(new RequestDto<string>
            {
                Parameter = Name
            });
            if (result.IsSuccess)//ServiceRequestTypeName is Exist!
            {
                return Json(result.Message);
            }
            return Json(true);
        }
        #endregion

    }
}
