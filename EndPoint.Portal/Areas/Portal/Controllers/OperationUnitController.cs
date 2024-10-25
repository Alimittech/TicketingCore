using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Commands.ChangeOpunitDep;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.CreateOperationUnit;
using Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.UpdateOperationUnit;
using Aliasys.Common.Dtos;
using Aliasys.Common.ExtentionMethods;
using Aliasys.Common.Paginations;
using EndPoint.Portal.Areas.Portal.Models.ViewModels.OperationUnitViewModel;
using EndPoint.Portal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area(nameof(Portal))]
    [Authorize]
    public class OperationUnitController : BaseController
    {
        private readonly IOperationUnitFacad _operationUnitFacad;
        private readonly IOperationUnitDependencyFacad _opunitDependencyFacad;
        public OperationUnitController(IOperationUnitFacad operationUnitFacad, IOperationUnitDependencyFacad opunitDependencyFacad)
        {
            _operationUnitFacad = operationUnitFacad;
            _opunitDependencyFacad = opunitDependencyFacad;
        }

        #region List Action
        //---------- List Action ----------
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getOpunit = _operationUnitFacad.GetOperationUnitFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getOpunit.ActionType == ActionType.Completed || getOpunit.ActionType == ActionType.Null)
            {
                string opuintListPath = "~/Areas/Portal/Views/OperationUnit/Components/DataTableOperationUnit/DataTableOperationUnitViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, opuintListPath, getOpunit.Data),
                    message = getOpunit.Message,
                    dataUpdate = "#resultDataTableOperationUnit"
                });
            }
            return Json(new { isValid = false, message = getOpunit.Message, errorMessage = true });
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
                    return ViewComponent("CreateOperationUnit");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateOperationUnitDto request)
        {
            if (ModelState.IsValid)
            {
                var createOpunit = _operationUnitFacad.CreateOperationUnitService.Create(new RequestDto<RequestCreateOperationUnitDto>
                {
                    Parameter = new RequestCreateOperationUnitDto
                    {
                        Code = request.Code,
                        Name = request.Name,
                    }
                });
                if (createOpunit.IsSuccess)
                {
                    var getOpunit = _operationUnitFacad.GetOperationUnitFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getOpunit.ActionType == ActionType.Completed || getOpunit.ActionType == ActionType.Null)
                    {
                        string opunitListPath = "~/Areas/Portal/Views/OperationUnit/Components/DataTableOperationUnit/DataTableOperationUnitViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, opunitListPath, getOpunit.Data),
                            message = createOpunit.Message,
                            dataUpdate = "#DataTableOperationUnit"
                        });
                    }
                }
                return Json(new { isValid = false, message = createOpunit.Message, errorMessage = true });
            }
            string opunitCreatePath = "~/Areas/Portal/Views/OperationUnit/Components/CreateOperationUnit/CreateOperationUnitViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, opunitCreatePath, request) });
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
                    var opunitFind = _operationUnitFacad.FindOperationUnitWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (opunitFind.IsSuccess)
                    {
                        var model = new RequestUpdateOperationUnitDto
                        {
                            Id = opunitFind.Data.Id,
                            Name = opunitFind.Data.Name,
                            Code = opunitFind.Data.Code,
                        };
                        return ViewComponent("UpdateOperationUnit", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = opunitFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateOperationUnitDto request)
        {
            if (ModelState.IsValid)
            {
                var updateOpunit = _operationUnitFacad.UpdateOperationUnitService.Update(new RequestDto<RequestUpdateOperationUnitDto>
                {
                    Parameter = request
                });
                if (updateOpunit.IsSuccess)
                {
                    var getOpunit = _operationUnitFacad.GetOperationUnitFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getOpunit.ActionType == ActionType.Completed || getOpunit.ActionType == ActionType.Null)
                    {
                        string opunitListPath = "~/Areas/Portal/Views/OperationUnit/Components/DataTableOperationUnit/DataTableOperationUnitViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, opunitListPath, getOpunit.Data),
                            message = updateOpunit.Message,
                            dataUpdate = "#DataTableOperationUnit"
                        });
                    }
                    return Json(new { isValid = false, message = getOpunit.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateOpunit.Message, errorMessage = true });
            }
            string opunitUpdatePath = "~/Areas/Portal/Views/OperationUnit/Components/UpdateOperationUnit/UpdateOperationUnitViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, opunitUpdatePath, request) });
        }
        #endregion

        //---------- Check Action ----------
        #region Check Action
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckOperationUnitName(string Name)
        {
            var result = _operationUnitFacad.FindOperationUnitWithNameService.Find(new RequestDto<string>
            {
                Parameter = Name
            });
            if (result.IsSuccess)//OperationUnitName is Exist!
            {
                return Json(result.Message);
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckOperationUnitCode(int Code)
        {
            var result = _operationUnitFacad.FindOperationUnitWithIdService.Find(new RequestDto<int>
            {
                Parameter = Code
            });
            if (result.IsSuccess)//OperationUnitCode is Exist!
            {
                return Json(result.Message);
            }
            return Json(true);
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
                    ViewBag.RouteAction = "deleteOperationUnit";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var opunit = _operationUnitFacad.DeleteOperationUnitService.Delete(Id);
            if (opunit.IsSuccess)
            {
                var getOpunit = _operationUnitFacad.GetOperationUnitFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getOpunit.ActionType == ActionType.Completed || getOpunit.ActionType == ActionType.Null)
                {
                    string opunitListPath = "~/Areas/Portal/Views/OperationUnit/Components/DataTableOperationUnit/DataTableOperationUnitViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, opunitListPath, getOpunit.Data),
                        message = opunit.Message,
                        dataUpdate = "#DataTableOperationUnit"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getOpunit.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = opunit.Message });
        }
        #endregion

        //---------- Dependency Action ----------
        #region Dependency Action
        [HttpGet]
        public IActionResult Dependency(string httpVerb, int Id, string Name)
        {
            if (httpVerb == "GET")
            {
                if (CustomExtentionMethods.IsAjaxRequest(HttpContext.Request, httpVerb).IsSuccess)
                {
                    return ViewComponent("DependencyOperationUnit", new { id = Id, name = Name});
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Dependency(OperationUnitDependencyViewModel request)
        {
            if (ModelState.IsValid)
            {
                var change = _opunitDependencyFacad.ChangeOpunitDependencyService.Change(new RequestDto<RequestChangeOpunitDependencyDto>
                {
                    Parameter = new RequestChangeOpunitDependencyDto
                    {
                        Id = request.Id,
                        OrganizationId = request.OrganizationId,
                        OperationUnitId = request.OperationUnitId,
                        ParentOperationUnitId = request.ParentOperationUnitId,
                        ManagerId = request.ManagerId,
                    }
                });
                if (change.IsSuccess)
                {
                    return Json(new { isValid = true, message = change.Message, errorMessage = false });
                }
                return Json(new { isValid = false, message = change.Message, errorMessage = true });
            }
            return Json(new { isValid = false, message = "Model is not valid!", errorMessage = true });
        }
        #endregion
    }
}
