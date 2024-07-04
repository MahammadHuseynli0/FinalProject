using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
