using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.PoductColorDTOs
{
    public class ProductColorCreateDTO
    {
        public string HexCode { get; set; }
        public string Name { get; set; }

    }

    public class ProductColorCreateDTOValidator : AbstractValidator<ProductColorCreateDTO>
    {
        public ProductColorCreateDTOValidator()
        {
            RuleFor(x => x.HexCode)
                .NotEmpty().WithMessage("HexCode is required")
                .NotNull().WithMessage("HexCode is required");

            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required")
               .NotNull().WithMessage("Name is required");
        }
    }
}
