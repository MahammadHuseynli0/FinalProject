using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;
using NanoNexus.Core.RepositoryAbstracts;
using NanoNexus.Data.DAL;
using NanoNexus.ViewModels;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NanoNexus.Controllers
{

    public class ShopController : Controller
    {

        private readonly IProductService _productService;
        private readonly IProductColorService _productColorService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketItemService _basketItemService;
        private readonly AppDbContext _appDbContext;
        private readonly IBasketItemRepository _basketItemRepository;

        public ShopController(IProductService productService, IProductColorService productColorService, IBrandService brandService, IMapper mapper, ICategoryService categoryService, UserManager<AppUser> userManager, IBasketItemService basketItemService, AppDbContext appDbContext, IBasketItemRepository basketItemRepository)
        {

            _productService = productService;
            _productColorService = productColorService;
            _brandService = brandService;
            _mapper = mapper;
            _categoryService = categoryService;
            _userManager = userManager;
            _basketItemService = basketItemService;
            _appDbContext = appDbContext;
            _basketItemRepository = basketItemRepository;
        }

        public IActionResult Index(string? search, decimal? minPrice, decimal? maxPrice, List<int> selectedBrands, List<int> selectedColors, int? categoryid, int? order, int page = 1)
        {

            var products = _productService.GetAllProducts(x => x.IsDeleted == false).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(x => x.Name.ToLower().Contains(search.Trim().ToLower()) ||
                                               x.Category.Name.ToLower().Contains(search.Trim().ToLower()) ||
                                               x.ProductColor.Name.ToLower().Contains(search.ToLower()) ||
                                               x.Brand.Name.ToLower().Contains(search.Trim().ToLower()));
            }

            if (selectedBrands != null && selectedBrands.Any())
            {
                products = products.Where(x => selectedBrands.Contains(x.Brand.Id));
            }

            if (selectedColors != null && selectedColors.Any())
            {
                products = products.Where(x => selectedColors.Contains(x.ProductColor.Id));
            }

            if (categoryid != null)
            {
                products = products.Where(x => x.Category.Id ==  categoryid);
            }

            if (minPrice.HasValue)
            {
                products = products.Where(x => x.Price >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(x => x.Price <= maxPrice);
            }




            if (order != null)
            {
                switch (order)
                {
                    case 1:
                        products = products.OrderByDescending(x => x.Price);
                        break;
                   
                    case 2:
                        products = products.OrderBy(x => x.Name);
                        break;
                    case 3:
                        products = products.OrderByDescending(x => x.IsNewArrivals);
                        break;
                    default:
                        break;
                }
            }


            List<Product> productGetDTOs = _mapper.Map<List<Product>>(products);

            //if (page <= 0 || page > (double)Math.Ceiling((double)productGetDTOs.Count / 2))
            //{
            //	return RedirectToAction("Index", "ErrorPage");
            //}
            var paginatedDatas = PaginatedList<Product>.Create(productGetDTOs, 12, page);

            ShopVm shopVm = new ShopVm
            {
                Colors = _productColorService.GetAllProductColors(x => x.IsDeleted == false),
                Brands = _brandService.GetAllBrands(x => x.IsDeleted == false),
                Categories = _categoryService.GetAllCategories(x => x.IsDeleted == false),
                PaginatedProducts = paginatedDatas,
                Products = _productService.GetAllProducts(x => x.IsDeleted == false)


            };


            return View(shopVm);
        }


        public IActionResult Detail(int id)
        {
            var products = _productService.GetAllProducts(x => x.IsDeleted == false).AsQueryable();

            var product = _productService.GetProduct(x => x.Id == id && x.IsDeleted == false);
            ShopVm shopVm = new ShopVm
            {

                Product = product,
                Colors = _productColorService.GetAllProductColors(x => x.IsDeleted == false),
                Products = products.Where(x => product.Category != null && x.Category != null && x.Category.Id == product.Category.Id).ToList(),

            };
            return View(shopVm);
        }


        public async Task<IActionResult> AddToBasket(int productId)
        {

            ProductGetDTO product = _productService.GetProduct(x => x.Id == productId);

            if (product == null) return NotFound();

            List<BasketItemVm> BasketItemViewModels = new List<BasketItemVm>();
            BasketItemVm BasketItemViewModel = null;
            BasketItem userBasketItem = null;
            AppUser user = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];

                if (string.IsNullOrEmpty(basketItemsStr))
                {
                    BasketItemViewModels = new List<BasketItemVm>();
                }
                else
                {
                    try
                    {
                        BasketItemViewModels = JsonConvert.DeserializeObject<List<BasketItemVm>>(basketItemsStr);
                    }
                    catch (JsonSerializationException ex)
                    {
                        Console.WriteLine("Error deserializing cookie: " + ex.Message);
                        return BadRequest("Invalid cookie format");
                    }
                }

                BasketItemViewModel = BasketItemViewModels.FirstOrDefault(x => x.ProductId == productId);

                if (BasketItemViewModel != null)
                {
                    BasketItemViewModel.Count++;
                }
                else
                {
                    BasketItemViewModel = new BasketItemVm()
                    {
                        ProductId = productId,
                        Count = 1
                    };
                    BasketItemViewModels.Add(BasketItemViewModel);
                }

                basketItemsStr = JsonConvert.SerializeObject(BasketItemViewModels);

                HttpContext.Response.Cookies.Append("BasketItems", basketItemsStr);
            }
            else
            {
                userBasketItem = _basketItemService.GetBasketItem(x => x.ProductId == productId &&
                x.UserId == user.Id && !x.IsDeleted);
                if (userBasketItem != null)
                {
                    userBasketItem.Counter++;
                    await _basketItemRepository.CommitAsync();
                }
                else
                {
                    userBasketItem = new BasketItem
                    {
                        ProductId = productId,
                        Counter = 1,
                        UserId = user.Id,
                        IsDeleted = false
                    };
                    await _basketItemService.AddBasketItem(userBasketItem);
                }

            }



            return Ok();

        }

        public async Task<IActionResult> CheckOut()
        {
          

            List<CheckOutVm> checkOutVms = new List<CheckOutVm>();
            List<BasketItemVm> basketItemVms = new List<BasketItemVm>();
            List<BasketItem> userBasketItems = new List<BasketItem>();
            CheckOutVm checkOutVm = null;
            AppUser user = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];

                if (basketItemsStr != null)
                {
                    basketItemVms = JsonConvert.DeserializeObject<List<BasketItemVm>>(basketItemsStr);

                    foreach (var item in basketItemVms)
                    {
                        checkOutVm = new CheckOutVm
                        {
                            Product = _productService.GetProduct(x => x.Id == item.ProductId),
                            Count = item.Count
                        };

                        checkOutVms.Add(checkOutVm);
                    }
                }
            }
            else
            {

                userBasketItems = _basketItemService.GetAllBasketItems(x => x.UserId ==
                user.Id && !x.IsDeleted).ToList();

                foreach (var item in userBasketItems)
                {
                    checkOutVm = new CheckOutVm
                    {
                        Product = _mapper.Map<ProductGetDTO>(item.Product),
                        Count = item.Counter
                    };
                    checkOutVms.Add(checkOutVm);
                }
            }

            OrderVm orderViewModel = new OrderVm
            {
                CheckOutVms = checkOutVms,
                Name = user?.Name,
                Surname = user?.Surname,
            };

            ViewBag.CheckOutViewModel = checkOutVms;

            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderVm orderViewModel)
        {


            if (!ModelState.IsValid)
                return View();

            List<CheckOutVm> checkOutVms = new List<CheckOutVm>();
            List<BasketItemVm> basketItemVms = new List<BasketItemVm>();
            List<BasketItem> userBasketItems = new List<BasketItem>();
            CheckOutVm checkOutVm = null;
            OrderItem orderItem = null;
            AppUser user = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            Order order = new Order
            {
                Name = orderViewModel.Name,
                Surname = orderViewModel.Surname,
                Address = orderViewModel.Address,
                Country = orderViewModel.Country,
                Email = orderViewModel.Email,
                Phone = orderViewModel.Phone,
                ZipCode = orderViewModel.ZipCode,
                Note = orderViewModel.Note,
                OrderItems = new List<OrderItem>(),
                AppUserId = user?.Id,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };

            order.OrderItems = new List<OrderItem>();

            if (user == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];

                if (basketItemsStr != null)
                {
                    basketItemVms = JsonConvert.DeserializeObject<List<BasketItemVm>>(basketItemsStr);

                    order.TotalPrice = 0;

                    foreach (var item in basketItemVms)
                    {
                        checkOutVm = new CheckOutVm
                        {
                            Product = _productService.GetProduct(x => x.Id == item.ProductId),
                            Count = item.Count
                        };
                        checkOutVms.Add(checkOutVm);
                        Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == item.ProductId);


                        orderItem = new OrderItem
                        {
                            Product = product,
                            ProductName = product.Name,
                            DiscountPercent = product.DiscountPercent,
                            Price = product.Price - product.Price * product.DiscountPercent / 100,
                            Count = item.Count,
                            Order = order
                        };

                        order.TotalPrice += orderItem.Price * orderItem.Count;

                        order.OrderItems.Add(orderItem);

                    }

                    if (ModelState.IsValid)
                        HttpContext.Response.Cookies.Delete("BasketItems");
                    else
                    {
                        ViewBag.CheckOutViewModel = checkOutVms;
                        return View();
                    }
                }
            }
            else
            {
                userBasketItems = _basketItemService.GetAllBasketItems(x => x.UserId ==
                    user.Id && x.IsDeleted == false).ToList();

                order.TotalPrice = 0;

                foreach (var item in userBasketItems)
                {
                    checkOutVm = new CheckOutVm
                    {
                        Product = _productService.GetProduct(x => x.Id == item.ProductId),
                        Count = item.Counter
                    };
                    checkOutVms.Add(checkOutVm);

                    Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == item.ProductId);

                    if (product.DiscountPercent != 0)
                    {
                        orderItem = new OrderItem
                        {
                            Product = product,
                            ProductName = product.Name,
                            DiscountPercent = product.DiscountPercent,
                            Price = product.Price - product.Price * product.DiscountPercent / 100,
                            Count = item.Counter,
                            Order = order
                        };
                    }
                    else
                    {
                        orderItem = new OrderItem
                        {
                            Product = product,
                            ProductName = product.Name,
                            DiscountPercent = product.DiscountPercent,
                            Price = product.Price,
                            Count = item.Counter,
                            Order = order
                        };
                    }



                    order.TotalPrice += orderItem.Price * orderItem.Count;
                    order.OrderItems.Add(orderItem);

                    item.IsDeleted = true;
                }
            }

            ViewBag.CheckOutViewModel = checkOutVms;
            if (!ModelState.IsValid)
                return View();


            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ShowBasket()
        {

            List<BasketItemVm> products = new();
            AppUser user = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user == null)
            {
                var baskets = HttpContext.Request.Cookies["BasketItems"];
                if (baskets != null)
                {
                    products = JsonConvert.DeserializeObject<List<BasketItemVm>>(baskets);
                    foreach (var item in products)
                    {
                        var existProduct = _productService.GetProduct(x => x.Id == item.ProductId);
                        if (existProduct != null)
                        {
                            item.Name = existProduct.Name;
                            item.Price = existProduct.Price;
                            item.ImageUrl = existProduct.ProductImages.FirstOrDefault(x => x.IsPoster)?.ImageUrl;
                            item.DiscountPercent = existProduct.DiscountPercent;
                        }
                    }
                }
            }
            else
            {

                var basketItems = _basketItemService.GetAllBasketItems(x => x.UserId ==
                  user.Id && !x.IsDeleted);

                if (basketItems != null)
                {
                    foreach (var basketItem in basketItems)
                    {
                        var existProduct = _productService.GetProduct(x => x.Id == basketItem.ProductId);
                        if (existProduct != null)
                        {
                            products.Add(new BasketItemVm
                            {
                                ProductId = basketItem.ProductId,
                                Count = basketItem.Counter,
                                Name = existProduct.Name,
                                Price = existProduct.Price,
                                ImageUrl = existProduct.ProductImages.FirstOrDefault(x => x.IsPoster)?.ImageUrl,
                                DiscountPercent = existProduct.DiscountPercent
                            }); ;
                        }
                    }
                }
            }

            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            AppUser user = null;


            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user == null)
            {
                // Unauthenticated user
                var baskets = HttpContext.Request.Cookies["BasketItems"];
                if (baskets != null)
                {
                    var products = JsonConvert.DeserializeObject<List<BasketItemVm>>(baskets);
                    var itemToRemove = products.FirstOrDefault(x => x.ProductId == productId);
                    if (itemToRemove != null)
                    {
                        products.Remove(itemToRemove);
                        var updatedBasket = JsonConvert.SerializeObject(products);
                        HttpContext.Response.Cookies.Append("BasketItems", updatedBasket);
                    }
                }
            }
            else
            {

                _basketItemService.DeleteBasketItem(productId, user.Id);

            }

            return RedirectToAction("ShowBasket");
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseItem(int productId)
        {
            AppUser user = null;
            BasketItem basketItem = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user == null)
            {

                var baskets = HttpContext.Request.Cookies["BasketItems"];
                if (baskets != null)
                {
                    var products = JsonConvert.DeserializeObject<List<BasketItemVm>>(baskets);
                    var itemToUpdate = products.FirstOrDefault(x => x.ProductId == productId);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate.Count++;
                        var updatedBasket = JsonConvert.SerializeObject(products);
                        HttpContext.Response.Cookies.Append("BasketItems", updatedBasket);
                    }
                }

            }
            else
            {
                basketItem = _basketItemService.GetBasketItem(x => x.UserId ==
                  user.Id && x.ProductId == productId && !x.IsDeleted);

                if (basketItem != null)
                {
                    basketItem.Counter++;
                    _basketItemRepository.Commit();
                }

            }

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> DecreaseItem(int productId)
        {
            AppUser user = null;
            BasketItem basketItem = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user == null)
            {

                var baskets = HttpContext.Request.Cookies["BasketItems"];
                if (baskets != null)
                {
                    var products = JsonConvert.DeserializeObject<List<BasketItemVm>>(baskets);
                    var itemToUpdate = products.FirstOrDefault(x => x.ProductId == productId);
                    if (itemToUpdate != null && itemToUpdate.Count > 1)
                    {
                        itemToUpdate.Count--;
                        var updatedBasket = JsonConvert.SerializeObject(products);
                        HttpContext.Response.Cookies.Append("BasketItems", updatedBasket);
                    }
                }

            }
            else
            {
                basketItem = _basketItemService.GetBasketItem(x => x.UserId ==
                  user.Id && x.ProductId == productId && !x.IsDeleted);

                if (basketItem != null && basketItem.Counter > 1)
                {
                    basketItem.Counter--;
                    _basketItemRepository.Commit();
                }

            }

            return Ok();
        }
    }
}


