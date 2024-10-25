using Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemFullList;

namespace EndPoint.Portal.Areas.Portal.Models.ViewModels.SystemViewModels
{
    public class DataTableSystemViewModel
    {
        public List<GetSystemViewModel> sysList { get; set; }
        public int RowsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
