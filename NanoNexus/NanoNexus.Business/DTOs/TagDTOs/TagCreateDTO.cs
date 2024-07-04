using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.TagDTOs
{
    public class TagCreateDTO
    {
        public string Name { get; set; }
    }

    public class TagCreateDTOValidator : AbstractValidator<TagCreateDTO>
    {
        public TagCreateDTOValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required")
             .NotNull().WithMessage("Name is required");
        }
    }
}
