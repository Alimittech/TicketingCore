using System.ComponentModel.DataAnnotations;

namespace EndPoint.Portal.Areas.Portal.Models.ViewModels.OperationUnitViewModel
{
    public class OperationUnitDependencyViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public int OrganizationId { get; set; }

        [Required]
        [Display(Name = "Operation Unit")]
        public int OperationUnitId { get; set; }

        [Required]
        [Display(Name = "Parent Operation Unit")]
        public int ParentOperationUnitId { get; set; }

        [Required]
        [Display(Name = "Manager")]
        public int ManagerId { get; set; }
    }
}
