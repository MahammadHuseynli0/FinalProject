using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.DTOs.SliderDTOs
{

    public class SliderCreateDTO
    {

        public IFormFile ImageFile { get; set; }

    }

  
}
