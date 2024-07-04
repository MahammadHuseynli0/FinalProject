using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.RepositoryAbstracts
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {

        Task AddEntityAsync(T entity);
        void SoftDelete(T entity);
        void DeleteEntity(T entity);
        void ReturnEntity(T entity);
        T GetEntity(Func<T, bool>? func = null, params string[]? includes);
        List<T> GetAllEntities(Func<T, bool>? func = null, params string[]? includes);
        int Commit();
        Task<int> CommitAsync();
    }
}
