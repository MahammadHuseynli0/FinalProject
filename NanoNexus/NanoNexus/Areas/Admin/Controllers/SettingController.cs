using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Business.DTOs.SettingDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Business.Service.Concretes;
using NanoNexus.Core.Models;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;

        public SettingController(ISettingService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var settings = _settingService.GetAllSettings(x => x.IsDeleted == false);
            List<Setting> settingGetDTOs = _mapper.Map<List<Setting>>(settings);

            var paginatedDatas = PaginatedList<Setting>.Create(settingGetDTOs, 2, page);

            return View(paginatedDatas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SettingCreateDTO settingCreateDTO)
        {
            await _settingService.AddSettingAsync(settingCreateDTO);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _settingService.DeleteSetting(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        public IActionResult Update(int id)
        {
            var existSetting = _settingService.GetSetting(x => x.Id == id);
            if (existSetting is null)
            {
                return NotFound();
            }

            SettingUpdateDTO settingUpdateDTO = new SettingUpdateDTO
            {
                Key = existSetting.Key,
                Value = existSetting.Value

            };
            return View(settingUpdateDTO);
        }
        [HttpPost]
        public IActionResult Update(SettingUpdateDTO settingUpdateDTO)
        {
            if (settingUpdateDTO is null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _settingService.UpdateSetting(settingUpdateDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");

        }

        public IActionResult Return(int id)
        {
            var setting = _settingService.GetSetting(x => x.Id == id);
            if (setting == null) return NotFound();
            try
            {
                _settingService.ReturnSetting(id);

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public IActionResult SoftDelete(int id)
        {
            var setting = _settingService.GetSetting(x => x.Id == id);
            if (setting == null) return NotFound();
            try
            {
                _settingService.SoftDelete(id);

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Archive()
        {
            var faqs = _settingService.GetAllSettings(x => x.IsDeleted == true);

            return View(faqs);
        }
    }
}
