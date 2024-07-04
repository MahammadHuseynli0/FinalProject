using AutoMapper;
using NanoNexus.Business.DTOs.CategoryDTOs;
using NanoNexus.Business.DTOs.CategoryDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;
using NanoNexus.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(CategoryCreateDTO CategoryCreateDTO)
        {
            Category Category = _mapper.Map<Category>(CategoryCreateDTO);

            await _categoryRepository.AddEntityAsync(Category);
            await _categoryRepository.CommitAsync();
        }

        public void DeleteCategory(int id)
        {
            var existCategory = _categoryRepository.GetEntity(x => x.Id == id);
            if (existCategory == null) throw new EntityNotFoundException("Category not found");

            _categoryRepository.DeleteEntity(existCategory);
            _categoryRepository.Commit();
        }

        public List<CategoryGetDTO> GetAllCategories(Func<Category, bool>? func = null)
        {
            var Categorys = _categoryRepository.GetAllEntities(func);
            List<CategoryGetDTO> CategoryDto = _mapper.Map<List<CategoryGetDTO>>(Categorys);


            return CategoryDto;
        }

        public CategoryGetDTO GetCategory(Func<Category, bool>? func = null)
        {
            var Category = _categoryRepository.GetEntity(func);
            CategoryGetDTO CategoryDto = _mapper.Map<CategoryGetDTO>(Category);


            return CategoryDto;
        }

        public void ReturnCategory(int id)
        {
            var existCategory = _categoryRepository.GetEntity(x => x.Id == id);
            if (existCategory == null) throw new EntityNotFoundException("Category not found!");

            _categoryRepository.ReturnEntity(existCategory);

            _categoryRepository.Commit();
        }

        public void SoftDelete(int id)
        {
            var existCategory = _categoryRepository.GetEntity(x => x.Id == id);
            if (existCategory == null) throw new EntityNotFoundException("Category not found!");
            existCategory.DeletedDate = DateTime.UtcNow.AddHours(4);

            _categoryRepository.SoftDelete(existCategory);

            _categoryRepository.Commit();
        }

        public void UpdateCategory(CategoryUpdateDTO updateDTO)
        {
            var oldCategory = _categoryRepository.GetEntity(x => x.Id == updateDTO.Id);
            if (oldCategory == null) throw new EntityNotFoundException("Category not found!");
            oldCategory.UpdatedDate = DateTime.UtcNow.AddHours(4);


            oldCategory.Name = updateDTO.Name;
            oldCategory.Icon = updateDTO.Icon;

            _categoryRepository.Commit();
        }
    }
}
