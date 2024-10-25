using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.CreateServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.UpdateServicePhase;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.FacadPatterns;
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
    public class ServicePhaseController : BaseController
    {
        private readonly IServicePhaseFacad _servicePhaseFacad;
        public ServicePhaseController(IServicePhaseFacad servicePhaseFacad)
        {
            _servicePhaseFacad = servicePhaseFacad;
        }

        //---------- List Action ----------
        #region List Action
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getServicePhase = _servicePhaseFacad.GetServicePhaseFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getServicePhase.ActionType == ActionType.Completed || getServicePhase.ActionType == ActionType.Null)
            {
                string servicePhaseListPath = "~/Areas/Portal/Views/ServicePhase/Components/DataTableServicePhase/DataTableServicePhaseViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, servicePhaseListPath, getServicePhase.Data),
                    message = getServicePhase.Message,
                    dataUpdate = "#DataTableServicePhase"
                });
            }
            return Json(new { isValid = false, message = getServicePhase.Message, errorMessage = true });
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
                    return ViewComponent("CreateServicePhase");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateServicePhaseDto request)
        {
            if (ModelState.IsValid)
            {
                var createServicePhase = _servicePhaseFacad.CreateServicePhaseService.Create(new RequestDto<RequestCreateServicePhaseDto>
                {
                    Parameter = new RequestCreateServicePhaseDto
                    {
                        ServiceRequestTypeId_FK = request.ServiceRequestTypeId_FK,
                        PhaseName = request.PhaseName,
                    }
                });
                if (createServicePhase.IsSuccess)
                {
                    var getServicePhase = _servicePhaseFacad.GetServicePhaseFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServicePhase.ActionType == ActionType.Completed || getServicePhase.ActionType == ActionType.Null)
                    {
                        string servicePhaseListPath = "~/Areas/Portal/Views/ServicePhase/Components/DataTableServicePhase/DataTableServicePhaseViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, servicePhaseListPath, getServicePhase.Data),
                            message = createServicePhase.Message,
                            dataUpdate = "#DataTableServicePhase"
                        });
                    }
                }
                return Json(new { isValid = false, message = createServicePhase.Message, errorMessage = true });
            }
            string servicePhaseCreatePath = "~/Areas/Portal/Views/ServicePhase/Components/CreateServicePhase/CreateServicePhaseViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, servicePhaseCreatePath, request) });
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
                    var servicePhaseFind = _servicePhaseFacad.FindServicePhaseWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (servicePhaseFind.IsSuccess)
                    {
                        var model = new RequestUpdateServicePhaseDto
                        {
                            Id = servicePhaseFind.Data.Id,
                            ServiceRequestTypeId_FK = servicePhaseFind.Data.ServiceRequestTypeId_FK,
                            PhaseName = servicePhaseFind.Data.PhaseName,
                        };
                        return ViewComponent("UpdateServicePhase", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = servicePhaseFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateServicePhaseDto request)
        {
            if (ModelState.IsValid)
            {
                var updateServicePhase = _servicePhaseFacad.UpdateServicePhaseService.Update(new RequestDto<RequestUpdateServicePhaseDto>
                {
                    Parameter = request
                });
                if (updateServicePhase.IsSuccess)
                {
                    var getServicePhase = _servicePhaseFacad.GetServicePhaseFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getServicePhase.ActionType == ActionType.Completed || getServicePhase.ActionType == ActionType.Null)
                    {
                        string servicePhaseListPath = "~/Areas/Portal/Views/ServicePhase/Components/DataTableServicePhase/DataTableServicePhaseViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, servicePhaseListPath, getServicePhase.Data),
                            message = updateServicePhase.Message,
                            dataUpdate = "#DataTableServicePhase"
                        });
                    }
                    return Json(new { isValid = false, message = getServicePhase.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateServicePhase.Message, errorMessage = true });
            }
            string servicePhaseUpdatePath = "~/Areas/Portal/Views/ServicePhase/Components/DataTableServicePhase/DataTableServicePhaseViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, servicePhaseUpdatePath, request) });
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
                    ViewBag.RouteAction = "deleteServicePhase";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var servicePhase = _servicePhaseFacad.DeleteServicePhaseService.Delete(Id);
            if (servicePhase.IsSuccess)
            {
                var getServicePhase = _servicePhaseFacad.GetServicePhaseFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getServicePhase.ActionType == ActionType.Completed || getServicePhase.ActionType == ActionType.Null)
                {
                    string servicePhaseListPath = "~/Areas/Portal/Views/ServicePhase/Components/DataTableServicePhase/DataTableServicePhaseViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, servicePhaseListPath, getServicePhase.Data),
                        message = servicePhase.Message,
                        dataUpdate = "#DataTableServicePhase"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getServicePhase.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = servicePhase.Message });
        }
        #endregion

        //---------- Check Action ----------
        #region Check Action
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckServicePhaseName(string Name)
        {
            var result = _servicePhaseFacad.FindServicePhaseWithNameService.Find(new RequestDto<string>
            {
                Parameter = Name
            });
            if (result.IsSuccess)//ServicePhaseName is Exist!
            {
                return Json(result.Message);
            }
            return Json(true);
        }
        #endregion

    }
}
