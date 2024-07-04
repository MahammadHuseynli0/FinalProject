using Microsoft.EntityFrameworkCore;
using NanoNexus.Core.Models;
using NanoNexus.Core.RepositoryAbstracts;
using NanoNexus.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Data.RepositoryConcretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddEntityAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public int Commit()
        {
            return _appDbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void DeleteEntity(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public List<T> GetAllEntities(Func<T, bool>? func = null, params string[]? includes)
        {
            var entity = _appDbContext.Set<T>().AsQueryable();
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    entity = entity.Include(include);
                }
            }

            return func == null ? entity.ToList() : entity.Where(func).ToList();
        }

        public T GetEntity(Func<T, bool>? func = null, params string[]? includes)
        {
            var entity = _appDbContext.Set<T>().AsQueryable();
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    entity = entity.Include(include);
                }
            }

            return func == null ? entity.FirstOrDefault() : entity.FirstOrDefault(func);
        }

        public void ReturnEntity(T entity)
        {
            entity.IsDeleted = false;
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
        }
    }
}
