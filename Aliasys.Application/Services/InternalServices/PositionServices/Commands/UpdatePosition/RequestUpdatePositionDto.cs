using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.PositionServices.Commands.UpdatePosition
{
    public class RequestUpdatePositionDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        //[Remote("checkPositionName")]
        public string Name { get; set; }
    }
}
