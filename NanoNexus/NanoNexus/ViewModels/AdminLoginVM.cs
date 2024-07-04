using System.ComponentModel.DataAnnotations;

namespace NanoNexus.ViewModels
{
    public class AdminLoginVm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
