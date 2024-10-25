using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.UserRollServices.Commands.UpdateUserRoll
{
    public class RequestUpdateUserRollDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        //[Remote("checkPositionName")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
