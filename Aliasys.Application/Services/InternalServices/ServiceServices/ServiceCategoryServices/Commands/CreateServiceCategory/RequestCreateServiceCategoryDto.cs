using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.CreateServiceCategory
{
    public class RequestCreateServiceCategoryDto
    {
        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Group Name")]
        public int UserGroupId_FK { get; set; }
    }
}
