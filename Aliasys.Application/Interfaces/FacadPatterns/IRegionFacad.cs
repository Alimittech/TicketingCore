using Aliasys.Application.Interfaces.Operations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IRegionFacad
    {
        IGetService<List<SelectListItem>> GetRegionSelectListService { get; }
    }
}
