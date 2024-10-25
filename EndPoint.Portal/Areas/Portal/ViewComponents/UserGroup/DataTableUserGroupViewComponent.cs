using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.UserGroup
{
    public class DataTableUserGroupViewComponent : ViewComponent
    {
        private readonly IUserGroupFacad _userGroupFacad;
        public DataTableUserGroupViewComponent(IUserGroupFacad userGroupFacad)
        {
            _userGroupFacad = userGroupFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var userGroupList = _userGroupFacad.GetUserGroupFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    SearchKey = searchKey,
                    Page = page,
                    PageSize = pageSize
                }
            });
            return View("~/Areas/Portal/Views/UserGroup/Components/DataTableUserGroup/DataTableUserGroupViewComponent.cshtml", userGroupList.Data);
        }
    }
}
