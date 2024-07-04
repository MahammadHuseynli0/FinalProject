using AutoMapper;
using NanoNexus.Business.DTOs.TagDTOs;
using NanoNexus.Business.Exceptions;
using NanoNexus.Business.Service.Abstracts;
using NanoNexus.Core.Models;
using NanoNexus.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Concretes
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task AddTagAsync(TagCreateDTO tagCreateDTO)
        {
            Tag tag = _mapper.Map<Tag>(tagCreateDTO);

            await _tagRepository.AddEntityAsync(tag);
            await _tagRepository.CommitAsync();
        }

        public void DeleteTag(int id)
        {
            var existTag = _tagRepository.GetEntity(x => x.Id == id);
            if (existTag == null) throw new EntityNotFoundException("Tag not found");

            _tagRepository.DeleteEntity(existTag);
            _tagRepository.Commit();
        }

        public List<TagGetDTO> GetAllTags(Func<Tag, bool>? func = null)
        {
            var tags = _tagRepository.GetAllEntities(func);
            List<TagGetDTO> tagGetDTOs = _mapper.Map<List<TagGetDTO>>(tags);

            return tagGetDTOs;
        }

        public TagGetDTO GetTag(Func<Tag, bool>? func = null)
        {
            var tag = _tagRepository.GetEntity(func);
            TagGetDTO tagGetDTO = _mapper.Map<TagGetDTO>(tag);

            return tagGetDTO;
        }

        public void ReturnTag(int id)
        {
            var existTag = _tagRepository.GetEntity(x => x.Id == id);
            if (existTag == null) throw new EntityNotFoundException("Tag not found!");


            _tagRepository.ReturnEntity(existTag);

            _tagRepository.Commit();
        }

        public void SoftDelete(int id)
        {
            var existTag = _tagRepository.GetEntity(x => x.Id == id);
            if (existTag == null) throw new EntityNotFoundException("Tag not found!");

            existTag.DeletedDate = DateTime.UtcNow.AddHours(4);

            _tagRepository.SoftDelete(existTag);

            _tagRepository.Commit();
        }

        public void UpdateTag(TagUpdateDTO updateDTO)
        {
            var existTag = _tagRepository.GetEntity(x => x.Id == updateDTO.Id);
            if (existTag == null) throw new EntityNotFoundException("Tag not found!");

            Tag tag = _mapper.Map<Tag>(updateDTO);

            existTag.Name = updateDTO.Name;


            _tagRepository.Commit();
        }
    }
}
