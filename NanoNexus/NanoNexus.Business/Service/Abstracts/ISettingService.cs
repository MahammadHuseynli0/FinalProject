using NanoNexus.Business.DTOs.SettingDTOs;
using NanoNexus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Service.Abstracts
{
    public interface ISettingService
    {
        Task AddSettingAsync(SettingCreateDTO settingCreateDTO);
        void DeleteSetting(int id);
        void UpdateSetting(SettingUpdateDTO settingUpdateDTO);
        SettingGetDTO GetSetting(Func<Setting, bool>? func = null);
        List<SettingGetDTO> GetAllSettings(Func<Setting, bool>? func = null);
        Task<Dictionary<string, string>> GetSettingsAsync();
        void SoftDelete(int id);
        void ReturnSetting(int id);
    }
}
