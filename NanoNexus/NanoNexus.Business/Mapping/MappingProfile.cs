using AutoMapper;
using NanoNexus.Business.DTOs.BrandDTOs;
using NanoNexus.Business.DTOs.CategoryDTOs;
using NanoNexus.Business.DTOs.PoductColorDTOs;
using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Business.DTOs.SettingDTOs;
using NanoNexus.Business.DTOs.SliderDTOs;
using NanoNexus.Business.DTOs.TagDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SliderCreateDTO, Slider>().ReverseMap();
            CreateMap<Slider, SliderGetDTO>().ReverseMap();
            CreateMap<SliderUpdateDTO, Slider>().ReverseMap();


            CreateMap<CategoryCreateDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryGetDTO>().ReverseMap();
            CreateMap<CategoryUpdateDTO, Category>().ReverseMap();

            CreateMap<ProductCreateDTO, Product>().ReverseMap();
            CreateMap<Product, ProductGetDTO>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();

            CreateMap<SettingCreateDTO, Setting>().ReverseMap();
            CreateMap<Setting, SettingGetDTO>().ReverseMap();
            CreateMap<SettingUpdateDTO, Setting>().ReverseMap();


            CreateMap<ProductColorCreateDTO, ProductColor>().ReverseMap();
            CreateMap<ProductColor, ProductColorGetDTO>().ReverseMap();
            CreateMap<ProductColorUpdateDTO, ProductColor>().ReverseMap();

            CreateMap<BrandCreateDTO, Brand>().ReverseMap();
            CreateMap<Brand, BrandGetDTO>().ReverseMap();
            CreateMap<BrandUpdateDTO, Brand>().ReverseMap();

            CreateMap<TagCreateDTO, Tag>().ReverseMap();
            CreateMap<Tag, TagGetDTO>().ReverseMap();
            CreateMap<TagUpdateDTO, Tag>().ReverseMap();
            


        }
    }
}
