using System.ComponentModel.DataAnnotations;

namespace NanoNexus.ViewModels
{
    public class MemberLoginVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    
    }
}
