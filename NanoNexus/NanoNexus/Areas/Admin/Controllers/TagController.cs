using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoNexus.Business.DTOs.TagDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Extension;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1)
        {
            var tags = _tagService.GetAllTags(x => x.IsDeleted == false);
            List<Tag> tagGetDtos = _mapper.Map<List<Tag>>(tags);

            var paginatedTags = PaginatedList<Tag>.Create(tagGetDtos, 2, page);

            return View(paginatedTags);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagCreateDTO tagCreateDTO)
        {
            if (!ModelState.IsValid)
                return View();

            await _tagService.AddTagAsync(tagCreateDTO);


            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existTag = _tagService.GetTag(x => x.Id == id);

            if (existTag == null) return NotFound();

            TagUpdateDTO tagUpdateDTO = new TagUpdateDTO
            {
                Name = existTag.Name
            };




            return View(tagUpdateDTO);
        }

        [HttpPost]
        public IActionResult Update(TagUpdateDTO tagUpdateDTO)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _tagService.UpdateTag(tagUpdateDTO);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            try
            {
                _tagService.DeleteTag(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        public IActionResult SoftDelete(int id)
        {
            try
            {
                _tagService.SoftDelete(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Return(int id)
        {
            try
            {
                _tagService.ReturnTag(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Archive()
        {
            var Tags = _tagService.GetAllTags(x => x.IsDeleted == true);

            return View(Tags);
        }
    }
}
