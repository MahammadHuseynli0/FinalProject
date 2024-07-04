using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public bool IsBestSeler { get; set; }
        public bool IsTop { get; set; }
        public bool IsNewArrivals { get; set; }
        public string TechnicalSpecs { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal CostPrice { get; set; }
        public int BrandId { get; set; }
        public IFormFile ProductPosterImageFile { get; set; }
        public List<IFormFile>? ImageFiles { get; set; }
        public List<int>? TagIds { get; set; }
        public int CategoryId { get; set; }
        public int ProductColorId { get; set; }


    }
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required");

            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required");

            RuleFor(x => x.ShortDescription)
            .NotEmpty().WithMessage("ShortDescription is required")
            .NotNull().WithMessage("ShortDescription is required");

            RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .NotNull().WithMessage("Price is required");

            RuleFor(x => x.CostPrice)
            .NotEmpty().WithMessage("CostPrice is required")
            .NotNull().WithMessage("CostPrice is required");

            RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required")
            .NotNull().WithMessage("Category is required");

            RuleFor(x => x.ProductColorId)
            .NotEmpty().WithMessage("ProductColor is required")
            .NotNull().WithMessage("ProductColor is required");

            RuleFor(x => x.BrandId)
            .NotEmpty().WithMessage("Brand is required")
            .NotNull().WithMessage("Brand is required");

            RuleFor(x => x.TechnicalSpecs)
            .NotEmpty().WithMessage("TechnicalSpecs is required")
            .NotNull().WithMessage("TechnicalSpecs is required");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.CostPrice > x.Price)
                {
                    context.AddFailure("Price", "The price cannot be cheaper than the price of cost!");
                }
            });
        }
    }
}
