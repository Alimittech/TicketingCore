using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using EndPoint.Portal.Areas.Portal.Models.ViewModels.RequestLifeCycleViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EndPoint.Portal.Areas.Portal.ViewComponents.ServiceRequestLifeCycle
{
    public class ChangeServiceRequestLifeCycleViewComponent : ViewComponent
    {
        private readonly IServiceRequestLifeCycleFacad _serviceRequestLifeCycleFacad;
        private readonly IUserInGroupFacad _userInGroupFacad;
        private readonly IServiceRootCauseFacad _serviceRootCauseFacad;
        private readonly IServiceSubCauseFacad _serviceSubCauseFacad;
        public ChangeServiceRequestLifeCycleViewComponent(IServiceRequestLifeCycleFacad serviceRequestLifeCycleFacad,
                                                          IUserInGroupFacad userInGroupFacad,
                                                          IServiceRootCauseFacad serviceRootCauseFacad,
                                                          IServiceSubCauseFacad serviceSubCauseFacad)
        {
            _serviceRequestLifeCycleFacad = serviceRequestLifeCycleFacad;
            _userInGroupFacad = userInGroupFacad;
            _serviceRootCauseFacad = serviceRootCauseFacad;
            _serviceSubCauseFacad = serviceSubCauseFacad;
        }
        public IViewComponentResult Invoke(long Id)
        {
            TempDataUserDto userInfo = JsonConvert.DeserializeObject<TempDataUserDto>(TempData["UserInfo"].ToString());
            TempData.Keep();

            var reqLifeCycle = _serviceRequestLifeCycleFacad.GetReqLifeCycleDependencyWithReqIdService.Get(new RequestDto<long>
            {
                Parameter = Id
            });

            var lastReqPhase = reqLifeCycle.Data.ServiceRequestLifeCycles.LastOrDefault();

            ViewBag.ProcessActionForHandle = Enum.GetValues(typeof(ProcessAction)).Cast<ProcessAction>()
                        .Except(new ProcessAction[] { ProcessAction.None })
                        .Except(new ProcessAction[] { ProcessAction.Resolved })
                        .Except(new ProcessAction[] { ProcessAction.Update })
                        .Except(new ProcessAction[] { ProcessAction.Assign })
                        .Select(p => new SelectListItem { Text = p.ToString(), Value = ((int)p).ToString() });

            ViewBag.ProcessActionForProcess = Enum.GetValues(typeof(ProcessAction)).Cast<ProcessAction>()
                        .Except(new ProcessAction[] { ProcessAction.None })
                        .Except(new ProcessAction[] { ProcessAction.Accept })
                        .Except(new ProcessAction[] { ProcessAction.Reject })
                        .Select(p => new SelectListItem { Text = p.ToString(), Value = ((int)p).ToString() });

            var getUserGroups = _userInGroupFacad.GetUserInGroupSelectListWithGrpNameService.Get(new RequestDto<string>
            {
                Parameter = reqLifeCycle.Data.ServiceRequest.ServiceCategory
            });
            if (getUserGroups != null)
            {
                ViewBag.UserInGroupList = new SelectList(getUserGroups.Data, "Value", "Text", 0);
            }

            var getRootCauses = _serviceRootCauseFacad.GetServiceRootCauseService.Get();
            if (getRootCauses != null)
            {
                ViewBag.RootCause = new SelectList(getRootCauses.Data, "Value", "Text", 0);
            }

            var getSubCause = _serviceSubCauseFacad.GetServiceActiveSubCauseService.Get();

            if (getSubCause != null)
            {

                ViewBag.SubCause = new SelectList(getSubCause.Data, "Value", "Text", 0);
            }

            //get all attachment file
            ChangeReqLifeCycleViewModel _changeReqLifeCycle = new ChangeReqLifeCycleViewModel();
            _changeReqLifeCycle.UserInfo = reqLifeCycle.Data.UserInfo;
            _changeReqLifeCycle.ServiceRequest = reqLifeCycle.Data.ServiceRequest;
            _changeReqLifeCycle.ServiceRequestLifeCycles = reqLifeCycle.Data.ServiceRequestLifeCycles;
            _changeReqLifeCycle.ProcessUserId = userInfo.UserId;
            return View("ChangeServiceRequestLifeCycleViewComponent", _changeReqLifeCycle);
        }
    }
}
