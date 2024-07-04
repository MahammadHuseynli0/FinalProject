using System.ComponentModel.DataAnnotations;

namespace NanoNexus.ViewModels
{
    public class MemberRegisterVm
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Your Name")]
        public string Name { get; set; }   

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [Display(Name = "New Password")]
        public string Password { get; set; }

    }
}
