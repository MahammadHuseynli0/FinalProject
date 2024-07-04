using NanoNexus.Business.DTOs.BrandDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{
    public interface IBrandService
    {
        Task AddBrandAsync(BrandCreateDTO brandCreateDTO);
        void DeleteBrand(int id);
        void SoftDelete(int id);
        void ReturnBrand(int id);
        void UpdateBrand(BrandUpdateDTO updateDTO);
        BrandGetDTO GetBrand(Func<Brand, bool>? func = null);
        List<BrandGetDTO> GetAllBrands(Func<Brand, bool>? func = null);
    }
}
