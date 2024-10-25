using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Common.Dtos
{
    public class CustomSelectListItem : SelectListItem
    {
        public int GroupId { get; set; }
    }
}
