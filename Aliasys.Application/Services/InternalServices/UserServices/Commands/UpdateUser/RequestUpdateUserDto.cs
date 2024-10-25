using System.ComponentModel.DataAnnotations;

namespace Aliasys.Application.Services.InternalServices.UserServices.Commands.UpdateUser
{
    public class RequestUpdateUserDto
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone No.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Ext No.")]
        public string ExtentionNumber { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
