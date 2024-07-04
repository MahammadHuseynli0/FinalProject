using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Business.DTOs.PoductColorDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class ProductColorController : Controller
    {
        private readonly IProductColorService _productColorService;
        private readonly IMapper _mapper;

        public ProductColorController(IProductColorService productColorService, IMapper mapper)
        {
            _productColorService = productColorService;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1)
        {
            var productColors = _productColorService.GetAllProductColors(x => x.IsDeleted == false);
            List<ProductColor> productColorGetDTOs = _mapper.Map<List<ProductColor>>(productColors);

            var paginatedDatas = PaginatedList<ProductColor>.Create(productColorGetDTOs, 3, page);
            return View(paginatedDatas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductColorCreateDTO productColorCreateDTO)
        {
            if (!ModelState.IsValid)
                return View();

            await _productColorService.AddProductColorAsync(productColorCreateDTO);


            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existProductColor = _productColorService.GetProductColor(x => x.Id == id);

            if (existProductColor == null) return NotFound();

            ProductColorUpdateDTO productUpdateDTO = new ProductColorUpdateDTO
            {
                Id = existProductColor.Id,
                HexCode = existProductColor.HexCode,
                Name = existProductColor.Name
            };




            return View(productUpdateDTO);
        }

        [HttpPost]
        public IActionResult Update(ProductColorUpdateDTO productColorUpdateDTO)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _productColorService.UpdateProductColor(productColorUpdateDTO);
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
                _productColorService.DeleteProductColor(id);
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
                _productColorService.SoftDelete(id);
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
                _productColorService.ReturnProductColor(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Archive()
        {
            var productColors = _productColorService.GetAllProductColors(x => x.IsDeleted == true);

            return View(productColors);
        }
    }
}
