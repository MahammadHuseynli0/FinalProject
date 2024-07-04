using NanoNexus.Business.DTOs.SliderDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{
    public interface ISliderService
    {
        Task AddSliderAsync(SliderCreateDTO createDTO);
        void DeleteSlider(int id);
        void SoftDelete(int id);
        void ReturnSlider(int id);
        void UpdateSlider(SliderUpdateDTO updateDTO);
        SliderGetDTO GetSlider(Func<Slider, bool>? func = null);
        List<SliderGetDTO> GetAllSliders(Func<Slider, bool>? func = null);
    }
}
