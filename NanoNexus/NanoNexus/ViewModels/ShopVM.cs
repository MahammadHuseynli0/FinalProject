using NanoNexus.Business.DTOs.BrandDTOs;
using NanoNexus.Business.DTOs.CategoryDTOs;
using NanoNexus.Business.DTOs.PoductColorDTOs;
using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Business.Extension;
using NanoNexus.Core.Models;

namespace NanoNexus.ViewModels
{
    public class ShopVm
    {
        public int? CategoryId { get; set; }
        public int? Order { get; set; }
        public string? Search {  get; set; }
        public List<ProductColorGetDTO> Colors = new List<ProductColorGetDTO>();
        public ProductGetDTO Product = new ProductGetDTO();
        public List<ProductGetDTO> Products = new List<ProductGetDTO>();
        public List<ProductColorGetDTO> ProductColor = new List<ProductColorGetDTO>();
        public List<BrandGetDTO> Brands = new List<BrandGetDTO>();
        public List<CategoryGetDTO> Categories = new List<CategoryGetDTO>();
        public PaginatedList<Product> PaginatedProducts = new PaginatedList<Product>();
        
       

    }
}
