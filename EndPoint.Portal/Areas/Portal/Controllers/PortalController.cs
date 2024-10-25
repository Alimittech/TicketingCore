using Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.GetOperationUnit.GetOpUnitFullList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EndPoint.Portal.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Authorize]
    public class PortalController : Controller
    {
        private readonly SubSystemInfoConfigDto _subSystemInfoConfigDto;

        public PortalController(SubSystemInfoConfigDto subSystemInfoConfigDto) 
        {
            _subSystemInfoConfigDto = subSystemInfoConfigDto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_subSystemInfoConfigDto);
        }
    }
}
