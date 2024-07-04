using NanoNexus.Business.DTOs.TagDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{
    public interface ITagService
    {
        Task AddTagAsync(TagCreateDTO tagCreateDTO);
        void DeleteTag(int id);
        void SoftDelete(int id);
        void ReturnTag(int id);
        void UpdateTag(TagUpdateDTO updateDTO);
        TagGetDTO GetTag(Func<Tag, bool>? func = null);
        List<TagGetDTO> GetAllTags(Func<Tag, bool>? func = null);
    }
}
