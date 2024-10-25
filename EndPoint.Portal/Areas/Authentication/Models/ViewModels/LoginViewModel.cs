using System.ComponentModel.DataAnnotations;

namespace EndPoint.Portal.Areas.Authentication.Models.ViewModels
{
    public class LoginViewModel
    {
        public string DomainName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
