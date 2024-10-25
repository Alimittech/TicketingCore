using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Services.InternalServices.SystemServices.FacadPatterns;
using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.CreateUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.UpdateUserRoll;
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
    public class UserRollController : BaseController
    {
        private readonly IUserRollFacad _rollFacad;
        public UserRollController(IUserRollFacad rollFacad)
        {
            _rollFacad = rollFacad;
        }

        //---------- List Action ----------
        #region List Action
        [HttpGet]
        public IActionResult List(string searchKey, int page, int pageSize)
        {
            var getRoll = _rollFacad.GetUserRollFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchKey = searchKey
                }
            });
            if (getRoll.ActionType == ActionType.Completed || getRoll.ActionType == ActionType.Null)
            {
                string rollListPath = "~/Areas/Portal/Views/UserRoll/Components/DataTableUserRoll/DataTableUserRollViewComponent.cshtml";
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, rollListPath, getRoll.Data),
                    message = getRoll.Message,
                    dataUpdate = "#resultDataTableRoll"
                });
            }
            return Json(new { isValid = false, message = getRoll.Message, errorMessage = true });
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
                    return ViewComponent("CreateUserRoll");
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Create(RequestCreateUserRollDto request)
        {
            if (ModelState.IsValid)
            {
                var createRoll = _rollFacad.CreateUserRollService.Create(new RequestDto<RequestCreateUserRollDto>
                {
                    Parameter = new RequestCreateUserRollDto
                    {
                        RollName = request.RollName,
                        Description = request.Description,
                    }
                });
                if (createRoll.IsSuccess)
                {
                    var getRoll = _rollFacad.GetUserRollFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getRoll.ActionType == ActionType.Completed || getRoll.ActionType == ActionType.Null)
                    {
                        string rollListPath = "~/Areas/Portal/Views/UserRoll/Components/DataTableUserRoll/DataTableUserRollViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, rollListPath, getRoll.Data),
                            message = createRoll.Message,
                            dataUpdate = "#DataTableRoll"
                        });
                    }
                }
                return Json(new { isValid = false, message = createRoll.Message, errorMessage = true });
            }
            string rollCreatePath = "~/Areas/Portal/Views/UserRoll/Components/DataTableUserRoll/DataTableUserRollViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, rollCreatePath, request) });
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
                    var rollFind = _rollFacad.FindUserRollWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = Id
                    });

                    if (rollFind.IsSuccess)
                    {
                        var model = new RequestUpdateUserRollDto
                        {
                            Id = rollFind.Data.Id,
                            Name = rollFind.Data.Name,
                            Description = rollFind.Data.Description,
                        };
                        return ViewComponent("UpdateUserRoll", model);
                    }
                    return Json(new { isValid = false, errorMessage = true, message = rollFind.Message });
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Update(RequestUpdateUserRollDto request)
        {
            if (ModelState.IsValid)
            {
                var updateRoll = _rollFacad.UpdateUserRollService.Update(new RequestDto<RequestUpdateUserRollDto>
                {
                    Parameter = request
                });
                if (updateRoll.IsSuccess)
                {
                    var getRoll = _rollFacad.GetUserRollFullListService.Get(new RequestDto<PaginationDto>
                    {
                        Parameter = new PaginationDto
                        {
                            Page = 1,
                            PageSize = 5,
                            SearchKey = string.Empty
                        }
                    });
                    if (getRoll.ActionType == ActionType.Completed || getRoll.ActionType == ActionType.Null)
                    {
                        string rollListPath = "~/Areas/Portal/Views/UserRoll/Components/DataTableUserRoll/DataTableUserRollViewComponent.cshtml";
                        return Json(new
                        {
                            isValid = true,
                            html = Helper.RenderRazorViewToString(this, rollListPath, getRoll.Data),
                            message = updateRoll.Message,
                            dataUpdate = "#DataTableRoll"
                        });
                    }
                    return Json(new { isValid = false, message = getRoll.Message, errorMessage = true });
                }
                return Json(new { isValid = false, message = updateRoll.Message, errorMessage = true });
            }
            string rollUpdatePath = "~/Areas/Portal/Views/UserRoll/Components/DataTableUserRoll/DataTableUserRollViewComponent.cshtml";
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, rollUpdatePath, request) });
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
                    ViewBag.RouteAction = "deleteRoll";
                    return PartialView("_PartialDelete", Id);
                }
            }
            return RedirectPermanent("/error");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var roll = _rollFacad.DeleteUserRollService.Delete(Id);
            if (roll.IsSuccess)
            {
                var getRoll = _rollFacad.GetUserRollFullListService.Get(new RequestDto<PaginationDto>
                {
                    Parameter = new PaginationDto
                    {
                        Page = 1,
                        PageSize = 5,
                        SearchKey = string.Empty
                    }
                });
                if (getRoll.ActionType == ActionType.Completed || getRoll.ActionType == ActionType.Null)
                {
                    string rollListPath = "~/Areas/Portal/Views/Roll/Components/DataTableRoll/DataTableRollViewComponent.cshtml";
                    return Json(new
                    {
                        isValid = true,
                        html = Helper.RenderRazorViewToString(this, rollListPath, getRoll.Data),
                        message = roll.Message,
                        dataUpdate = "#DataTableRoll"
                    });
                }
                return Json(new { isValid = false, errorMessage = true, message = getRoll.Message });
            }
            return Json(new { isValid = false, errorMessage = true, message = roll.Message });
        }
        #endregion
    }
}
