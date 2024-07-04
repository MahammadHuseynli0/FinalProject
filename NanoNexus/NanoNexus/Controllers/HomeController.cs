using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Data.DAL;
using NanoNexus.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace NanoNexus.Controllers
{
    public class HomeController : Controller
    {

            private readonly ISliderService _sliderService;
            private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _appDbContext;

        public HomeController(ISliderService sliderService, IProductService productService, ICategoryService categoryService, AppDbContext appDbContext)
        {
            _sliderService = sliderService;
            _productService = productService;
            _categoryService = categoryService;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
            {
                HomeVm homeVm = new HomeVm
                {
                    Products = _productService.GetAllProducts(x => x.IsDeleted == false),
                    Sliders = _sliderService.GetAllSliders(x => x.IsDeleted == false),
                    Categories = _categoryService.GetAllCategories(x => x.IsDeleted == false),
                    TopSlidetProducts = _productService.GetAllProducts(x => x.IsDeleted == false).Take(2).ToList(),
                };

                return View(homeVm);
            }
        public IActionResult Search(string search)
        {
            var products = _appDbContext.Products.Where(p => p.Name.ToLower().Contains(search.ToLower()))
                .Include(p=>p.Category)
                .Include(p=>p.ProductImages)
                .OrderByDescending(p=>p.Id)
                .Take(10)
                .ToList();

            return PartialView("_SearchPartial",products);
        }



    }
}
