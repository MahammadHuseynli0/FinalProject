using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.Models
{
    public class ContactPost : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Comment { get; set; }
        public string? Answer { get; set; }
        public DateTime? AnsweredDate { get; set; }
        public int? AnswerByUserId { get; set; }

    }
}
