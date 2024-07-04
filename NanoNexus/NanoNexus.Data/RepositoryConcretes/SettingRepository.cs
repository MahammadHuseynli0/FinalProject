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
    public class SettingRepository : GenericRepository<Setting>, ISettingRepository
    {
        private readonly AppDbContext _appDbContext;
        public SettingRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            var setting = await _appDbContext.Settings.Where(x => x.DeletedDate == null).ToDictionaryAsync(s => s.Key, s => s.Value);
            return setting;
        }
    }
}
