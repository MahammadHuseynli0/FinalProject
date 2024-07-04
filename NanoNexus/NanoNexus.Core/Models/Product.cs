using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.Models
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsBestSeler { get; set; }
        public bool IsTop { get; set; }
        public bool IsNewArrivals { get; set; }
        public string TechnicalSpecs { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal CostPrice { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<Tag> Tags { get; set; }
        public Category Category { get; set; }
        public int ProductColorId { get; set; }
        public ProductColor ProductColor { get; set; }
    }
}
