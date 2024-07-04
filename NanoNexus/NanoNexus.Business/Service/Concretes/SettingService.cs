using AutoMapper;
using NanoNexus.Business.DTOs.SettingDTOs;
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
    public class SettingService : ISettingService
    {

        private readonly ISettingRepository _repository;
        private readonly IMapper _mapper;
        public SettingService(ISettingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddSettingAsync(SettingCreateDTO settingCreateDTO)
        {
            Setting setting = _mapper.Map<Setting>(settingCreateDTO);

            await _repository.AddEntityAsync(setting);
            await _repository.CommitAsync();
        }

        public void DeleteSetting(int id)
        {
            var setting = _repository.GetEntity(x => x.DeletedDate == null && x.Id == id);
            if (setting is null)
            {
                throw new EntityNotFoundException("Setting Not Found!");
            }
            _repository.DeleteEntity(setting);
            _repository.Commit();
        }

        public List<SettingGetDTO> GetAllSettings(Func<Setting, bool>? func = null)
        {
            var settings = _repository.GetAllEntities(x => x.IsDeleted == false);
            List<SettingGetDTO> settingGetDTOs = _mapper.Map<List<SettingGetDTO>>(settings);
            return settingGetDTOs;
        }



        public SettingGetDTO GetSetting(Func<Setting, bool>? func = null)
        {
            var setting = _repository.GetEntity(x => x.IsDeleted == false);
            SettingGetDTO settingGetDTO = _mapper.Map<SettingGetDTO>(setting);
            return settingGetDTO;
        }
        public void ReturnSetting(int id)
        {
            var existSetting = _repository.GetEntity(x => x.Id == id);
            if (existSetting == null) throw new EntityNotFoundException("Setting not found!");

            _repository.ReturnEntity(existSetting);

            _repository.Commit();
        }

        public void SoftDelete(int id)
        {
            var existSetting = _repository.GetEntity(x => x.Id == id);
            if (existSetting == null) throw new EntityNotFoundException("Setting not found!");


            _repository.SoftDelete(existSetting);

            existSetting.DeletedDate = DateTime.UtcNow.AddHours(4);

            _repository.Commit();

        }

        public Task<Dictionary<string, string>> GetSettingsAsync()
        {
            return _repository.GetSettingsAsync();
        }

        public void UpdateSetting(SettingUpdateDTO settingUpdateDTO)
        {
            var oldSetting = _repository.GetEntity(x => x.DeletedDate == null && x.Id == settingUpdateDTO.Id);
            if (oldSetting is null)
            {
                throw new EntityNotFoundException("Setting Not Found!");
            }
            Setting setting = _mapper.Map<Setting>(settingUpdateDTO);

            oldSetting.Key = settingUpdateDTO.Key;
            oldSetting.Value = settingUpdateDTO.Value;
            oldSetting.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Commit();
        }
    }
}