
using System.ComponentModel.DataAnnotations;

namespace WeStrapApplication.Models
{
    public class LoginModel
    {

        [Display(Name = "USERNAME")]
        [Required(ErrorMessageResourceName = "USERNAME_REQUIRED", ErrorMessageResourceType = typeof(Resources.Models_LoginModel))]
        [StringLength(20, MinimumLength = 5, ErrorMessageResourceName = "MIN_LENGTH", ErrorMessageResourceType = typeof(Resources.Models_LoginModel))]
        public string UserName { get; set; }

        [Display(Name = "Mot de Passe")]
        [Required]
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }
    }
}
