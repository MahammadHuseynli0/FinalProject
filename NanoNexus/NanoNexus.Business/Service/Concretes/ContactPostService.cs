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
    public class ContactPostService : IContactPostService
    {
        private readonly IContactPostRepository _contactPostRepository;

        public ContactPostService(IContactPostRepository contactPostRepository)
        {
            _contactPostRepository = contactPostRepository;
        }

        public async Task AddContactPostAsync(ContactPost contactPost)
        {
            await _contactPostRepository.AddEntityAsync(contactPost);
            await _contactPostRepository.CommitAsync();
        }

        public void DeleteContactPost(int id)
        {
            var existContactPost = _contactPostRepository.GetEntity(x => x.Id == id);
            if (existContactPost == null) throw new EntityNotFoundException("ContactPost not found");

            _contactPostRepository.DeleteEntity(existContactPost);
            _contactPostRepository.Commit();
        }

        public List<ContactPost> GetAllContactPosts(Func<ContactPost, bool>? func = null)
        {
            return _contactPostRepository.GetAllEntities(func);
        }

        public ContactPost GetContactPost(Func<ContactPost, bool>? func = null)
        {
            return _contactPostRepository.GetEntity(func);
        }


        public void UpdateContactPost(ContactPost updateDTO)
        {
            var existContactPost = _contactPostRepository.GetEntity(x => x.Id == updateDTO.Id);
            if (existContactPost == null) throw new EntityNotFoundException("ContactPost not found");


            existContactPost.Answer = updateDTO.Answer;

            _contactPostRepository.Commit();
        }
    }
}
