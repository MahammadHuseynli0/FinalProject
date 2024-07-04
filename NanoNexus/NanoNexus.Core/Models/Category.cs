using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.Models
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }

        public string Icon { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
