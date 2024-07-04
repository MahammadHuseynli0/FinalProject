using FluentValidation;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.CategoryDTOs
{
    public class CategoryCreateDTO
    {

        public string Icon { get; set; }
        public string Name { get; set; }
   


    }

    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .NotNull().WithMessage("Name is required");

        }
    }
}
