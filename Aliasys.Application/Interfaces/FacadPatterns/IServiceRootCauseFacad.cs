using Aliasys.Application.Interfaces.Operations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IServiceRootCauseFacad
    {
        IGetService<List<SelectListItem>> GetServiceRootCauseService { get; }
        IGetService<string, int> GetRootCauseWithIdService { get; }
    }
}
