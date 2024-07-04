using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using NanoNexus.Business.DTOs.ProductDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;
using NanoNexus.Core.RepositoryAbstracts;
using NanoNexus.Data.RepositoryConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Concretes 
{
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly IProductImageRepository _productImageRepository;
    private readonly IProductColorRepository _productColorRepository;
    private readonly IBrandRepository _brandRepository;
    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment env, IMapper mapper, IProductImageRepository productImageRepository, IProductColorRepository productColorRepository, IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _env = env;
        _mapper = mapper;
        _productImageRepository = productImageRepository;
        _productColorRepository = productColorRepository;
        _brandRepository = brandRepository;
    }

    public async Task AddProduct(ProductCreateDTO productCreateDTO)
    {
        if (productCreateDTO.ProductPosterImageFile == null) throw new EntityFileNotFoundException("File not found!");
        var existGenre = _categoryRepository.GetEntity(x => x.Id == productCreateDTO.CategoryId);
        if (existGenre == null) throw new EntityNotFoundException("Category not found!");
        var existColor = _productColorRepository.GetEntity(x => x.Id == productCreateDTO.ProductColorId);
        if (existColor == null) throw new EntityNotFoundException("Color not found!");
        var existBrand = _brandRepository.GetEntity(x => x.Id == productCreateDTO.BrandId);
        if (existBrand == null) throw new EntityNotFoundException("Brand not found!");


        Product product = _mapper.Map<Product>(productCreateDTO);

        ProductImage productImage = new ProductImage
        {
            Product = product,
            ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads/products", productCreateDTO.ProductPosterImageFile, "product"),
            IsPoster = true
        };
        _productImageRepository.AddEntityAsync(productImage);


            if (productCreateDTO.ImageFiles != null)
            {
                foreach (var item in productCreateDTO.ImageFiles)
                {
                    ProductImage productImage1 = new ProductImage
                    {
                        Product = product,
                        ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads/products", item, "product"),
                        IsPoster = false
                    };
                    _productImageRepository.AddEntityAsync(productImage1);

                }
            }







        await _productRepository.AddEntityAsync(product);
        await _productRepository.CommitAsync();
    }

    public void DeleteProduct(int id)
    {
        var existProduct = _productRepository.GetEntity(x => x.Id == id);
        if (existProduct == null) throw new EntityNotFoundException("Product not found");
        var existroductImage = _productImageRepository.GetAllEntities(x => x.ProductId == id);
        if (existroductImage == null) throw new EntityNotFoundException("ProductImage not found");
        foreach (var item in existroductImage)
        {
            Helper.DeleteFile(_env.WebRootPath, @"uploads\products", item.ImageUrl);
        }


        _productRepository.DeleteEntity(existProduct);
        _productRepository.Commit();
    }

    public List<ProductGetDTO> GetAllProducts(Func<Product, bool>? func = null)
    {
        var products = _productRepository.GetAllEntities(func, "Category", "ProductImages", "ProductColor", "Brand");
        List<ProductGetDTO> productsDto = _mapper.Map<List<ProductGetDTO>>(products);

        return productsDto;
    }

    public ProductGetDTO GetProduct(Func<Product, bool>? func = null)
    {
        var product = _productRepository.GetEntity(func, "Category", "ProductImages", "ProductColor", "Brand");
        ProductGetDTO productsDto = _mapper.Map<ProductGetDTO>(product);



        return productsDto;
    }

    public void ReturnProduct(int id)
    {
        var existProduct = _productRepository.GetEntity(x => x.Id == id);
        if (existProduct == null) throw new EntityNotFoundException("Product not found!");


        _productRepository.ReturnEntity(existProduct);

        _productRepository.Commit();
    }



    public void SoftDelete(int id)
    {
        var existProduct = _productRepository.GetEntity(x => x.Id == id);
        if (existProduct == null) throw new EntityNotFoundException("Product not found!");
        existProduct.DeletedDate = DateTime.UtcNow.AddHours(4);

        _productRepository.SoftDelete(existProduct);

        _productRepository.Commit();
    }

    public void UpdateProduct(ProductUpdateDTO productUpdateDTO)
    {
        var oldProduct = _productRepository.GetEntity(x => x.Id == productUpdateDTO.Id, "ProductImages");

        if (oldProduct == null) throw new EntityNotFoundException("Product not found");
        var existGenre = _categoryRepository.GetEntity(x => x.Id == productUpdateDTO.CategoryId);
        if (existGenre == null) throw new EntityNotFoundException("Category not found!");
        var existColor = _productColorRepository.GetEntity(x => x.Id == productUpdateDTO.ProductColorId);
        if (existColor == null) throw new EntityNotFoundException("Color not found!");
        var existBrand = _brandRepository.GetEntity(x => x.Id == productUpdateDTO.BrandId);
        if (existBrand == null) throw new EntityNotFoundException("Brand not found!");

        Product product = _mapper.Map<Product>(productUpdateDTO);


        foreach (var imgd in oldProduct.ProductImages.Where(bi => !productUpdateDTO.ProductImageIds.Contains(bi.Id) && bi.IsPoster != true))
        {
            Helper.DeleteFile(_env.WebRootPath, @"uploads\products", imgd.ImageUrl);
        }

        oldProduct.ProductImages.RemoveAll(bi => !productUpdateDTO.ProductImageIds.Contains(bi.Id) && bi.IsPoster != true);


        if (productUpdateDTO.ProductPosterImageFile is not null)
        {

            ProductImage productImage = new ProductImage()
            {
                Product = oldProduct,
                ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\products", productUpdateDTO.ProductPosterImageFile, "product"),
                IsPoster = true
            };

            foreach (var item in oldProduct.ProductImages.Where(x => x.IsPoster == true))
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\products", item.ImageUrl);
            }

            oldProduct.ProductImages.Remove(oldProduct.ProductImages.Where(x => x.IsPoster == true).FirstOrDefault());


            oldProduct.ProductImages.Add(productImage);
        }

        if (productUpdateDTO.ImageFiles is not null)
        {


            foreach (var item in productUpdateDTO.ImageFiles)
            {
                ProductImage image = new ProductImage()
                {
                    Product = oldProduct,
                    ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\products", item, "product"),
                    IsPoster = false
                };



                oldProduct.ProductImages.Add(image);


            }


        }


        oldProduct.CategoryId = productUpdateDTO.CategoryId;
        oldProduct.Name = productUpdateDTO.Name;
        oldProduct.Description = productUpdateDTO.Description;
        oldProduct.ShortDescription = productUpdateDTO.ShortDescription;
        oldProduct.Price = productUpdateDTO.Price;
        oldProduct.DiscountPercent = productUpdateDTO.DiscountPercent;
        oldProduct.CostPrice = productUpdateDTO.CostPrice;
        oldProduct.CategoryId = productUpdateDTO.CategoryId;
        oldProduct.ProductColorId = productUpdateDTO.ProductColorId;
        oldProduct.BrandId = productUpdateDTO.BrandId;
        oldProduct.IsBestSeler = productUpdateDTO.IsBestSeler;
        oldProduct.IsTop = productUpdateDTO.IsTop;
        oldProduct.IsNewArrivals = productUpdateDTO.IsNewArrivals;
        oldProduct.TechnicalSpecs = productUpdateDTO.TechnicalSpecs;



        oldProduct.UpdatedDate = DateTime.UtcNow.AddHours(4);

        _productRepository.Commit();

    }

}
}

