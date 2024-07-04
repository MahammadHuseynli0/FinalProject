using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.BrandDTOs
{
    public class BrandCreateDTO
    {
        public string Name { get; set; }
    }

    public class BrandCreateDTOValidator : AbstractValidator<BrandCreateDTO>
    {
        public BrandCreateDTOValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required")
             .NotNull().WithMessage("Name is required");
        }
    }
}
