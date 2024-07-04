using NanoNexus.Business.DTOs.CategoryDTOs;
using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(CategoryCreateDTO categoryCreateDTO);
        void DeleteCategory(int id);
        void SoftDelete(int id);
        void ReturnCategory(int id);
        void UpdateCategory(CategoryUpdateDTO updateDTO);
        CategoryGetDTO GetCategory(Func<Category, bool>? func = null);
        List<CategoryGetDTO> GetAllCategories(Func<Category, bool>? func = null);
    }
}
