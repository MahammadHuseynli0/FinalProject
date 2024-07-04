using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.TagDTOs
{
    public class TagUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TagUpdateDTOValidator : AbstractValidator<TagUpdateDTO>
    {
        public TagUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required")
             .NotNull().WithMessage("Name is required");
        }
    }
}
