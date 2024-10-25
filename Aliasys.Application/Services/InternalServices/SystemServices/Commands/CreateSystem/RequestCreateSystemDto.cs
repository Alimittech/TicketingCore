using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.SystemServices.Commands.CreateSystem
{
    public class RequestCreateSystemDto
    {
        [Required]
        [Display(Name="Parent System")]
        public int ParentSystem { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
