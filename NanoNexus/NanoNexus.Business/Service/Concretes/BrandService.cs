using AutoMapper;
using NanoNexus.Business.DTOs.BrandDTOs;
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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;


        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task AddBrandAsync(BrandCreateDTO brandCreateDTO)
        {
            Brand brand = _mapper.Map<Brand>(brandCreateDTO);

            await _brandRepository.AddEntityAsync(brand);
            await _brandRepository.CommitAsync();
        }

        public void DeleteBrand(int id)
        {
            var existBrand = _brandRepository.GetEntity(x => x.Id == id);
            if (existBrand == null) throw new EntityNotFoundException("Brand not found");

            _brandRepository.DeleteEntity(existBrand);
            _brandRepository.Commit();
        }

        public List<BrandGetDTO> GetAllBrands(Func<Brand, bool>? func = null)
        {
            var brands = _brandRepository.GetAllEntities(func);
            List<BrandGetDTO> brandDto = _mapper.Map<List<BrandGetDTO>>(brands);


            return brandDto;
        }

        public BrandGetDTO GetBrand(Func<Brand, bool>? func = null)
        {
            var brand = _brandRepository.GetEntity(func);
            BrandGetDTO brandDto = _mapper.Map<BrandGetDTO>(brand);


            return brandDto;
        }

        public void ReturnBrand(int id)
        {
            var existBrand = _brandRepository.GetEntity(x => x.Id == id);
            if (existBrand == null) throw new EntityNotFoundException("Brand not found!");

            _brandRepository.ReturnEntity(existBrand);

            _brandRepository.Commit();
        }

        public void SoftDelete(int id)
        {
            var existBrand = _brandRepository.GetEntity(x => x.Id == id);
            if (existBrand == null) throw new EntityNotFoundException("Brand not found!");
            existBrand.DeletedDate = DateTime.UtcNow.AddHours(4);

            _brandRepository.SoftDelete(existBrand);

            _brandRepository.Commit();
        }

        public void UpdateBrand(BrandUpdateDTO updateDTO)
        {
            var oldBrand = _brandRepository.GetEntity(x => x.Id == updateDTO.Id);
            if (oldBrand == null) throw new EntityNotFoundException("Brand not found!");
            oldBrand.UpdatedDate = DateTime.UtcNow.AddHours(4);


            oldBrand.Name = updateDTO.Name;

            _brandRepository.Commit();
        }
    }
}
