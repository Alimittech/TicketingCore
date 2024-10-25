using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.User
{
    public class DataTableUserViewComponent : ViewComponent
    {
        private readonly IUserFacad _userFacad;
        public DataTableUserViewComponent(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var userList = _userFacad.GetUserFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto { SearchKey = searchKey, Page = page, PageSize = pageSize }
            });
            return View("~/Areas/Portal/Views/User/Components/DataTableUser/DataTableUserViewComponent.cshtml", userList.Data);
        }
    }
}
