using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Business.DTOs.BrandDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1)
        {

            var brands = _brandService.GetAllBrands(x => x.IsDeleted == false);
            List<Brand> brandGetDTOs = _mapper.Map<List<Brand>>(brands);

            var paginatedDatas = PaginatedList<Brand>.Create(brandGetDTOs, 2, page);
            return View(paginatedDatas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateDTO brandCreateDTO)
        {
            if (!ModelState.IsValid)
                return View();

            await _brandService.AddBrandAsync(brandCreateDTO);


            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existBrand = _brandService.GetBrand(x => x.Id == id);

            if (existBrand == null) return NotFound();

            BrandUpdateDTO brandUpdateDTO = new BrandUpdateDTO
            {
                Name = existBrand.Name
            };




            return View(brandUpdateDTO);
        }

        [HttpPost]
        public IActionResult Update(BrandUpdateDTO brandUpdateDTO)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _brandService.UpdateBrand(brandUpdateDTO);
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
                _brandService.DeleteBrand(id);
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
                _brandService.SoftDelete(id);
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
                _brandService.ReturnBrand(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Archive()
        {
            var brands = _brandService.GetAllBrands(x => x.IsDeleted == true);

            return View(brands);
        }

    }
}
