using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.CreatePosition;
using Aliasys.Application.Services.InternalServices.PositionServices.Commands.UpdatePosition;
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
    public class PositionController : BaseController
    {
        private readonly IPositionFacad _postionFacad;
        public PositionController(IPositionFacad positionFacad)
        {
            _postionFacad = positionFacad;
        }

        //---------- List Action ----------
        #region List Action
        //---------- List Action ----------
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getPosition = _postionFacad.GetPositionFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getPosition.ActionType == ActionType.Completed || getPosition.ActionType == ActionType.Null)
            {
                string posListPath = "~/Areas/Portal/Views/Position/Components/DataTablePosition/DataTablePositionViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, posListPath, getPosition.Data),
                    message = getPosition.Message,
                    dataUpdate = "#resultDataTablePosition"
                });
            }
            return Json(new { isValid = false, message = getPosition.Message, errorMessage = true });
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
                    return ViewComponent("CreatePosition");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreatePositionDto request)
        {
            if (ModelState.IsValid)
            {
                var createPosition = _postionFacad.CreatePositionService.Create(new RequestDto<RequestCreatePositionDto>
                {
                    Parameter = new RequestCreatePositionDto
                    {
                        Name = request.Name,
                    }
                });
                if (createPosition.IsSuccess)
                {
                    var getPosition = _postionFacad.GetPositionFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getPosition.ActionType == ActionType.Completed || getPosition.ActionType == ActionType.Null)
                    {
                        string posListPath = "~/Areas/Portal/Views/Position/Components/DataTablePosition/DataTablePositionViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, posListPath, getPosition.Data),
                            message = createPosition.Message,
                            dataUpdate = "#DataTablePosition"
                        });
                    }
                }
                return Json(new { isValid = false, message = createPosition.Message, errorMessage = true });
            }
            string posCreatePath = "~/Areas/Portal/Views/Position/Components/CreatePosition/CreatePositionViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, posCreatePath, request) });
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
                    var posFind = _postionFacad.FindPositionWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (posFind.IsSuccess)
                    {
                        var model = new RequestUpdatePositionDto
                        {
                            Id = posFind.Data.Id,
                            Name = posFind.Data.Name,
                        };
                        return ViewComponent("UpdatePosition", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = posFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdatePositionDto request)
        {
            if (ModelState.IsValid)
            {
                var updatePosition = _postionFacad.UpdatePositionService.Update(new RequestDto<RequestUpdatePositionDto>
                {
                    Parameter = request
                });
                if (updatePosition.IsSuccess)
                {
                    var getPosition = _postionFacad.GetPositionFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getPosition.ActionType == ActionType.Completed || getPosition.ActionType == ActionType.Null)
                    {
                        string positionListPath = "~/Areas/Portal/Views/Position/Components/DataTablePosition/DataTablePositionViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, positionListPath, getPosition.Data),
                            message = updatePosition.Message,
                            dataUpdate = "#DataTablePosition"
                        });
                    }
                    return Json(new { isValid = false, message = getPosition.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updatePosition.Message, errorMessage = true });
            }
            string posUpdatePath = "~/Areas/Portal/Views/Position/Components/UpdatePosition/UpdatePositionViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, posUpdatePath, request) });
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
                    ViewBag.RouteAction = "deletePosition";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var pos = _postionFacad.DeletePositionService.Delete(Id);
            if (pos.IsSuccess)
            {
                var getPos = _postionFacad.GetPositionFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getPos.ActionType == ActionType.Completed || getPos.ActionType == ActionType.Null)
                {
                    string posListPath = "~/Areas/Portal/Views/Position/Components/DataTablePosition/DataTablePositionViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, posListPath, getPos.Data),
                        message = pos.Message,
                        dataUpdate = "#DataTablePosition"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getPos.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = pos.Message });
        }
        #endregion
    }
}
