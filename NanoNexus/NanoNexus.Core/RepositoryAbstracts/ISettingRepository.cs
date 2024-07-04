﻿using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.RepositoryAbstracts
{
    public interface ISettingRepository : IGenericRepository<Setting>
    {
        Task<Dictionary<string, string>> GetSettingsAsync();
    }
}
