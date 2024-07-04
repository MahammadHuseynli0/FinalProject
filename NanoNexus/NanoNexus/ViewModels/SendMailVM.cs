using System.ComponentModel.DataAnnotations;

namespace NanoNexus.ViewModels
{
    public class SendMailVM
    {
        [Required]
        public string Name { get; set; }

        

        [Required]
        [MaxLength(200)]
        public string Comment { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
