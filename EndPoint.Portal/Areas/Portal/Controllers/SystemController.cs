using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.PositionServices.FacadPatterns;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.CreateSystem;
using Aliasys.Application.Services.InternalServices.SystemServices.Commands.UpdateSystem;
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
    public class SystemController : BaseController
    {
        private readonly ISystemFacad _systemFacad;
        public SystemController(ISystemFacad systemFacad)
        {
            _systemFacad = systemFacad;
        }

        //---------- Index Action ----------
        #region Index
        [HttpGet]
        public IActionResult Index(PaginationDto paginationDto)
        {
            ViewBag.searchKey = paginationDto.SearchKey;
            ViewBag.page = paginationDto.Page;
            ViewBag.pageSize = paginationDto.PageSize;
            return View();
        }
        #endregion


        #region List Action
        //---------- List Action ----------
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getSystem = _systemFacad.GetSystemFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getSystem.ActionType == ActionType.Completed || getSystem.ActionType == ActionType.Null)
            {
                string sysListPath = "~/Areas/Portal/Views/System/Components/DataTableSystem/DataTableSystemViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, sysListPath, getSystem.Data),
                    message = getSystem.Message,
                    dataUpdate = "#resultDataTableSystem"
                });
            }
            return Json(new { isValid = false, message = getSystem.Message, errorMessage = true });
        }
        #endregion


        //---------- Create Action ----------
        #region Create
        [HttpGet]
        public IActionResult Create(string httpVerb)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("CreateSystem");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateSystemDto request)
        {
            if (ModelState.IsValid)
            {
                var createSystem = _systemFacad.CreateSystemService.Create(new RequestDto<RequestCreateSystemDto>
                {
                    Parameter = request
                });
                if (createSystem.IsSuccess)
                {
                    var getSys = _systemFacad.GetSystemFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getSys.ActionType == ActionType.Completed || getSys.ActionType == ActionType.Null)
                    {
                        string sysListPath = "~/Areas/Portal/Views/System/Components/DataTableSystem/DataTableSystemViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, sysListPath, getSys.Data),
                            message = createSystem.Message,
                            dataUpdate = "#DataTableSystem"
                        });
                    }
                }
                //ModelState.AddModelError(request.Name, "The entered name is already exist!");
                return Json(new { isValid = false, message = createSystem.Message, errorMessage = true });
            }
            string SysCreatePath = "~/Areas/Portal/Views/System/Components/CreateSystem/CreateSystemViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, SysCreatePath, request) });
        }
        #endregion


        //---------- Update Action ----------
        #region Update
        [HttpGet]
        public IActionResult Update(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    var sysFind = _systemFacad.FindSystemWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (sysFind.IsSuccess)
                    {
                        var model = new RequestUpdateSystemDto
                        {
                            Id = sysFind.Data.Id,
                            ParentSystem = int.Parse(sysFind.Data.ParentSystem.Value),
                            Name = sysFind.Data.Name,
                            Description = sysFind.Data.Description
                        };
                        return ViewComponent("UpdateSystem", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = sysFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateSystemDto request)
        {
            if (ModelState.IsValid)
            {
                var updateSys = _systemFacad.UpdateSystemService.Update(new RequestDto<RequestUpdateSystemDto>
                {
                    Parameter = request
                });
                if (updateSys.IsSuccess)
                {
                    var getSys = _systemFacad.GetSystemFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getSys.ActionType == ActionType.Completed || getSys.ActionType == ActionType.Null)
                    {
                        string sysListPath = "~/Areas/Portal/Views/System/Components/DataTableSystem/DataTableSystemViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, sysListPath, getSys.Data),
                            message = updateSys.Message,
                            dataUpdate = "#DataTableSystem"
                        });
                    }
                    return Json(new { isValid = false, message = getSys.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateSys.Message, errorMessage = true });
            }
            string sysUpdatePath = "~/Areas/Portal/Views/System/Components/UpdateSystem/UpdateSystemViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, sysUpdatePath, request) });
        }
        #endregion


        //---------- Delete Action ----------
        #region Delete
        [HttpGet]
        public IActionResult Delete(string httpVerb, int Id)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    ViewBag.RouteAction = "deleteSystem";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var sys = _systemFacad.DeleteSystemService.Delete(Id);
            if (sys.IsSuccess)
            {
                var getSys = _systemFacad.GetSystemFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getSys.ActionType == ActionType.Completed || getSys.ActionType == ActionType.Null)
                {
                    string sysListPath = "~/Areas/Portal/Views/System/Components/DataTableSystem/DataTableSystemViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, sysListPath, getSys.Data),
                        message = sys.Message,
                        dataUpdate = "#DataTableSystem"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getSys.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = sys.Message });
        }
        #endregion

    }
}
