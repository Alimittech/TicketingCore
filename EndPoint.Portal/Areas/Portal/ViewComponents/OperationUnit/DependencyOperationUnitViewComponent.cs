using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using EndPoint.Portal.Areas.Portal.Models.ViewModels.OperationUnitViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.OperationUnit
{
    public class DependencyOperationUnitViewComponent : ViewComponent
    {
        private readonly IOperationUnitDependencyFacad _opunitDepFacad;
        private readonly IOrganizationFacad _organizationFacad;
        private readonly IOperationUnitFacad _operationUnitFacad;
        private readonly IUserFacad _userFacad;
        public DependencyOperationUnitViewComponent(IOrganizationFacad organizationFacad,
                                                    IOperationUnitFacad operationUnitFacad,
                                                    IUserFacad userFacad,
                                                    IOperationUnitDependencyFacad opunitDepFacad)
        {
            _organizationFacad = organizationFacad;
            _operationUnitFacad = operationUnitFacad;
            _userFacad = userFacad;
            _opunitDepFacad = opunitDepFacad;
        }

        public IViewComponentResult Invoke(int Id, string Name)//Id is for operation unit
        {
            var findOpunit = _opunitDepFacad.FindOpunitDepWithOpunitIdService.Find(new RequestDto<int>
            {
                Parameter = Id,
            });

            ViewBag.OpunitName = Name;

            var orgList = _organizationFacad.GetOrganizationSelectListService.Get();
            if (orgList.IsSuccess)
            {
                ViewBag.OrgList = new SelectList(orgList.Data, "Value", "Text", (findOpunit.IsSuccess) ? findOpunit.Data.OrganizationId : 0);
            }

            var opunitList = _operationUnitFacad.GetOperationUnitSelectListService.Get();
            if (opunitList.IsSuccess)
            {
                ViewBag.ParentOpunitList = new SelectList(opunitList.Data, "Value", "Text", (findOpunit.IsSuccess) ? findOpunit.Data.ParentOpunitId : 0);
            }

            var userList = _userFacad.GetUserSelectListService.Get();
            if (userList.IsSuccess)
            {
                ViewBag.UserList = new SelectList(userList.Data, "Value", "Text", (findOpunit.IsSuccess) ? findOpunit.Data.ManagerId : 0);
            }
            if (findOpunit.IsSuccess)
            {
                OperationUnitDependencyViewModel model = new OperationUnitDependencyViewModel();
                model.Id = findOpunit.Data.Id;
                model.OrganizationId = findOpunit.Data.OrganizationId;
                model.OperationUnitId = findOpunit.Data.OperationUnitId;
                model.ParentOperationUnitId = findOpunit.Data.ParentOpunitId;
                model.ManagerId = findOpunit.Data.ManagerId;
                return View("DependencyOperationUnitViewComponent", model);
            }
            return View("DependencyOperationUnitViewComponent");
        }
    }
}