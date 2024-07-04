using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Business.DTOs.CategoryDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1)
        {
            var categories = _categoryService.GetAllCategories(x => x.IsDeleted == false);
            List<Category> categoryGetDTOs = _mapper.Map<List<Category>>(categories);

            var paginatedDatas = PaginatedList<Category>.Create(categoryGetDTOs, 2, page);

            return View(paginatedDatas);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAllCategories(x => x.IsDeleted == false);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDTO categoryCreateDTO)
        {

            ViewBag.Categories = _categoryService.GetAllCategories(x => x.IsDeleted == false);
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _categoryService.AddCategoryAsync(categoryCreateDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            ViewBag.Categories = _categoryService.GetAllCategories(x => x.IsDeleted == false);
            var existCategory = _categoryService.GetCategory(x => x.Id == id);

            if (existCategory == null) return NotFound();

            CategoryUpdateDTO categoryUpdate = new CategoryUpdateDTO
            {
                Id = existCategory.Id,
                Name = existCategory.Name,
                Icon= existCategory.Icon   
            };




            return View(categoryUpdate);
        }

        [HttpPost]
        public IActionResult Update(CategoryUpdateDTO categoryUpdateDTO)
        {

            ViewBag.Categories = _categoryService.GetAllCategories(x => x.IsDeleted == false);
            if (!ModelState.IsValid)
                return View();

            try
            {
                _categoryService.UpdateCategory(categoryUpdateDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        public IActionResult SoftDelete(int id)
        {
            try
            {
                _categoryService.SoftDelete(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Return(int id)
        {
            try
            {
                _categoryService.ReturnCategory(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Archive()
        {
            var faqs = _categoryService.GetAllCategories(x => x.IsDeleted == true);

            return View(faqs);
        }

    }
}
