using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.SettingDTOs
{
    public class SettingUpdateDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class SettingUpdateDTODTOValidator : AbstractValidator<SettingUpdateDTO>
    {
        public SettingUpdateDTODTOValidator()
        {
            RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key is required")
            .NotNull().WithMessage("Key is required");

            RuleFor(x => x.Value)
               .NotEmpty().WithMessage("Value is required")
                 .NotNull().WithMessage("Value is required");
        }
    }
}