using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.CreateOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.FindOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgDependency;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.UpdateOrganization;
using Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgFullList;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IOrganizationFacad
    {
        IFindService<ResultFindOrganizationDto, string> FindOrganizationWithNameService { get; }
        IFindService<ResultFindOrganizationDto, int> FindOrganizationWithIdService { get; }
        IGetService<List<SelectListItem>> GetOrganizationSelectListService { get; }
        IGetService<ResultGetOrganizationFullListDto, PaginationDto> GetOrganizationFullListService { get; }
        IGetService<ResultGetOrganizationDependencyDto, int> GetOrganizationDependencyService { get; }
        ICreateService<int?, RequestCreateOrganizationDto> CreateOrganizationService { get; }
        IUpdateService<int?, RequestUpdateOrganizationDto> UpdateOrganizationService { get; }
        IDeleteService DeleteOrganizationService { get; }
    }
}
