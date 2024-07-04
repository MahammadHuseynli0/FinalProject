using NanoNexus.Business.DTOs.PoductColorDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{
    public interface IProductColorService
    {
        Task AddProductColorAsync(ProductColorCreateDTO productColorCreateDTO);
        void DeleteProductColor(int id);
        void SoftDelete(int id);
        void ReturnProductColor(int id);
        void UpdateProductColor(ProductColorUpdateDTO updateDTO);
        ProductColorGetDTO GetProductColor(Func<ProductColor, bool>? func = null);
        List<ProductColorGetDTO> GetAllProductColors(Func<ProductColor, bool>? func = null);
    }
}
