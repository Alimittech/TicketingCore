using System.ComponentModel.DataAnnotations;

namespace EndPoint.Portal.Areas.Portal.Models.ViewModels.SystemViewModels
{
    public class UpdateSystemViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Parent System")]
        public int ParentSystem { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
