using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{

    public interface IProductService
    {
        Task AddProduct(ProductCreateDTO productCreateDTO);
        void DeleteProduct(int id);
        void SoftDelete(int id);
        void ReturnProduct(int id);
        void UpdateProduct(ProductUpdateDTO productUpdateDTO);
        ProductGetDTO GetProduct(Func<Product, bool>? func = null);
        List<ProductGetDTO> GetAllProducts(Func<Product, bool>? func = null);
    }
}
