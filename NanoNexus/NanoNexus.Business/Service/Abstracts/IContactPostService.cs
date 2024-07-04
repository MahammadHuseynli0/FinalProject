using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{
    public interface IContactPostService
    {
        Task AddContactPostAsync(ContactPost contactPostCreateDTO);
        void DeleteContactPost(int id);
        void UpdateContactPost(ContactPost updateDTO);
        ContactPost GetContactPost(Func<ContactPost, bool>? func = null);
        List<ContactPost> GetAllContactPosts(Func<ContactPost, bool>? func = null);
    }
}
