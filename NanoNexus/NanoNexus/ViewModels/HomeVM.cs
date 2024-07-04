using NanoNexus.Business.DTOs.BrandDTOs;
using NanoNexus.Business.DTOs.CategoryDTOs;
using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Business.DTOs.SliderDTOs;

namespace NanoNexus.ViewModels
{
    public class HomeVm
    {
        public List<SliderGetDTO> Sliders = new List<SliderGetDTO>();
        public List<ProductGetDTO> Products = new List<ProductGetDTO>();
        public List<CategoryGetDTO> Categories = new List<CategoryGetDTO>();
        public List<ProductGetDTO> TopSlidetProducts = new List<ProductGetDTO>();

    }
}
