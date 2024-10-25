using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Services.InternalServices.SystemServices.Queries.FindSystem
{
    public class ResultFindSystemDto
    {
        public int Id { get; set; }
        public SelectListItem ParentSystem { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
