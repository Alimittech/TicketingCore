using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.Roll
{
    public class DataTableUserRollViewComponent : ViewComponent
    {
        private readonly IUserRollFacad _rollFacad;
        public DataTableUserRollViewComponent(IUserRollFacad rollFacad)
        {
            _rollFacad = rollFacad;
        }
        public IViewComponentResult Invoke(string searchKey, int page, int pageSize)
        {
            var rollList = _rollFacad.GetUserRollFullListService.Get(new RequestDto<PaginationDto>
            {
                Parameter = new PaginationDto
                {
                    SearchKey = searchKey,
                    Page = page,
                    PageSize = pageSize
                }
            });
            return View("~/Areas/Portal/Views/UserRoll/Components/DataTableUserRoll/DataTableUserRollViewComponent.cshtml", rollList.Data);
        }
    }
}
