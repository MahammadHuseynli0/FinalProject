using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.Models
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsPoster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
