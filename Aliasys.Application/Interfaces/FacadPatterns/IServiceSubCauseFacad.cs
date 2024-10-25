using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServiceSubCauseFacad
    {
        IGetService<List<SelectListItem>> GetServiceSubCauseService { get; }
        IGetService<List<CustomSelectListItem>> GetServiceActiveSubCauseService { get; }
        IGetService<string, int> GetSubCauseWithIdService { get; }
    }
}
