using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.SliderDTOs
{
    public class SliderUpdateDTO
    {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

}
