using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.CategoryDTOs
{
    public class CategoryGetDTO
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }

    }
}
