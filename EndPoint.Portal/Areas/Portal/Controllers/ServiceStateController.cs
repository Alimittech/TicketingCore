using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.CreateServiceState;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.UpdateServiceState;
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
    public class ServiceStateController : BaseController
    {
        private readonly IServiceStateFacad _serviceStateFacad;
        public ServiceStateController(IServiceStateFacad serviceStateFacad)
        {
            _serviceStateFacad = serviceStateFacad;
        }

        //---------- List Action ----------
        #region List Action
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getServiceState = _serviceStateFacad.GetServiceStateFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getServiceState.ActionType == ActionType.Completed || getServiceState.ActionType == ActionType.Null)
            {
                string serviceStateListPath = "~/Areas/Portal/Views/ServiceState/Components/DataTableServiceState/DataTableServiceStateViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, serviceStateListPath, getServiceState.Data),
                    message = getServiceState.Message,
                    dataUpdate = "#DataTableServiceState"
                });
            }
            return Json(new { isValid = false, message = getServiceState.Message, errorMessage = true });
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
                    return ViewComponent("CreateServiceState");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateServiceStateDto request)
        {
            if (ModelState.IsValid)
            {
                var createServiceState = _serviceStateFacad.CreateServiceStateService.Create(new RequestDto<RequestCreateServiceStateDto>
                {
                    Parameter = new RequestCreateServiceStateDto
                    {
                        ServiceRequestTypeId_FK = request.ServiceRequestTypeId_FK,
                        StateName = request.StateName,
                    }
                });
                if (createServiceState.IsSuccess)
                {
                    var getServiceState = _serviceStateFacad.GetServiceStateFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServiceState.ActionType == ActionType.Completed || getServiceState.ActionType == ActionType.Null)
                    {
                        string serviceStateListPath = "~/Areas/Portal/Views/ServiceState/Components/DataTableServiceState/DataTableServiceStateViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, serviceStateListPath, getServiceState.Data),
                            message = createServiceState.Message,
                            dataUpdate = "#DataTableServiceState"
                        });
                    }
                }
                return Json(new { isValid = false, message = createServiceState.Message, errorMessage = true });
            }
            string serviceStateCreatePath = "~/Areas/Portal/Views/ServiceState/Components/CreateServiceState/CreateServiceStateViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceStateCreatePath, request) });
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
                    var serviceStateFind = _serviceStateFacad.FindServiceStateWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (serviceStateFind.IsSuccess)
                    {
                        var model = new RequestUpdateServiceStateDto
                        {
                            Id = serviceStateFind.Data.Id,
                            ServiceRequestTypeId_FK = serviceStateFind.Data.ServiceRequestTypeId_FK,
                            StateName = serviceStateFind.Data.StateName,
                        };
                        return ViewComponent("UpdateServiceState", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = serviceStateFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateServiceStateDto request)
        {
            if (ModelState.IsValid)
            {
                var updateServiceState = _serviceStateFacad.UpdateServiceStateService.Update(new RequestDto<RequestUpdateServiceStateDto>
                {
                    Parameter = request
                });
                if (updateServiceState.IsSuccess)
                {
                    var getServiceState = _serviceStateFacad.GetServiceStateFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServiceState.ActionType == ActionType.Completed || getServiceState.ActionType == ActionType.Null)
                    {
                        string serviceStateListPath = "~/Areas/Portal/Views/ServiceState/Components/DataTableServiceState/DataTableServiceStateViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, serviceStateListPath, getServiceState.Data),
                            message = updateServiceState.Message,
                            dataUpdate = "#DataTableServiceState"
                        });
                    }
                    return Json(new { isValid = false, message = getServiceState.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateServiceState.Message, errorMessage = true });
            }
            string serviceStateUpdatePath = "~/Areas/Portal/Views/ServiceState/Components/DataTableServiceState/DataTableServiceStateViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, serviceStateUpdatePath, request) });
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
                    ViewBag.RouteAction = "deleteServiceState";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var serviceState = _serviceStateFacad.DeleteServiceStateService.Delete(Id);
            if (serviceState.IsSuccess)
            {
                var getServiceState = _serviceStateFacad.GetServiceStateFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getServiceState.ActionType == ActionType.Completed || getServiceState.ActionType == ActionType.Null)
                {
                    string serviceStateListPath = "~/Areas/Portal/Views/ServiceState/Components/DataTableServiceState/DataTableServiceStateViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, serviceStateListPath, getServiceState.Data),
                        message = serviceState.Message,
                        dataUpdate = "#DataTableServiceState"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getServiceState.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = serviceState.Message });
        }
        #endregion

        //---------- Check Action ----------
        #region Check Action
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckServiceStateName(string Name)
        {
            var result = _serviceStateFacad.FindServiceStateWithNameService.Find(new RequestDto<string>
            {
                Parameter = Name
            });
            if (result.IsSuccess)//ServiceStateName is Exist!
            {
                return Json(result.Message);
            }
            return Json(true);
        }
        #endregion
    }
}
