using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.ProductDTOs
{

    public class ProductGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public bool IsBestSeler { get; set; }
        public bool IsTop { get; set; }
        public bool IsNewArrivals { get; set; }
        public string TechnicalSpecs { get; set; }

        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public decimal CostPrice { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public ProductColor ProductColor { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<int> ProductImageIds { get; set; }


    }
}
