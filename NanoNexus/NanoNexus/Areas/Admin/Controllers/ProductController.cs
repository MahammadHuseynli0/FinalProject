using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductColorService _productColorService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ICategoryService categoryService, IProductColorService productColorService, IBrandService brandService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productColorService = productColorService;
            _brandService = brandService;
            _mapper = mapper;
        }

        public IActionResult Index(string search, int page = 1)
        {

            var product = search == null ? _productService.GetAllProducts(x => x.IsDeleted == false) : _productService.GetAllProducts(x => x.IsDeleted == false && x.Name.Contains(search)).ToList();
            var datas = _productService.GetAllProducts(x => x.IsDeleted == false);

            List<Product> productGetDTOs = _mapper.Map<List<Product>>(datas);

            var paginatedDatas = PaginatedList<Product>.Create(productGetDTOs, 2, page);

            return View(paginatedDatas);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAllCategories(x => x.IsDeleted == false);
            ViewBag.ProductColor = _productColorService.GetAllProductColors(x => x.IsDeleted == false);
            ViewBag.Brand = _brandService.GetAllBrands(x => x.IsDeleted == false);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO productCreateDTO)
        {

            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.ProductColor = _productColorService.GetAllProductColors(x => x.IsDeleted == false);
            ViewBag.Brand = _brandService.GetAllBrands(x => x.IsDeleted == false);

            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _productService.AddProduct(productCreateDTO);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFiles", ex.Message);
                return View();

            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFiles", ex.Message);
                return View();

            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var existProduct = _productService.GetProduct(x => x.Id == id);

            if (existProduct == null) return NotFound();
            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.ProductColor = _productColorService.GetAllProductColors(x => x.IsDeleted == false);
            ViewBag.Brand = _brandService.GetAllBrands(x => x.IsDeleted == false);

            ProductUpdateDTO productUpdateDTO = new ProductUpdateDTO
            {
                Name = existProduct.Name,
                Price = existProduct.Price,
                Description = existProduct.Description,
                ShortDescription = existProduct.ShortDescription,
                DiscountPercent = existProduct.DiscountPercent,
                CostPrice = existProduct.CostPrice,
                CategoryId = existProduct.Category.Id,
                ProductColorId = existProduct.ProductColor.Id,
                BrandId = existProduct.Brand.Id,
                ProductImages = existProduct.ProductImages,
                TechnicalSpecs = existProduct.TechnicalSpecs,
                IsTop = existProduct.IsTop,
                IsNewArrivals = existProduct.IsNewArrivals,
                IsBestSeler = existProduct.IsBestSeler



            };




            return View(productUpdateDTO);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateDTO productUpdateDTO)
        {
            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.ProductColor = _productColorService.GetAllProductColors(x => x.IsDeleted == false);
            ViewBag.Brand = _brandService.GetAllBrands(x => x.IsDeleted == false);
            //if(productUpdateDTO.ProductImageIds.Count  == 0 )
            //{
            //    ModelState.AddModelError("", "Siline bilmez");
            //    return View();
            //}
            if (!ModelState.IsValid)
                return View();

            try
            {
                _productService.UpdateProduct(productUpdateDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
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
                _productService.DeleteProduct(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (EntityFileNotFoundException ex)
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
                _productService.SoftDelete(id);
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
                _productService.ReturnProduct(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Archive()
        {
            var products = _productService.GetAllProducts(x => x.IsDeleted == true);

            return View(products);
        }
    }
}
