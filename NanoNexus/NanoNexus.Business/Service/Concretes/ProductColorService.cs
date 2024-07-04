using AutoMapper;
using NanoNexus.Business.DTOs.PoductColorDTOs;
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
    public class ProductColorService : IProductColorService
    {
        private readonly IProductColorRepository _productColorRepository;
        private readonly IMapper _mapper;

        public ProductColorService(IProductColorRepository productColorRepository, IMapper mapper)
        {
            _productColorRepository = productColorRepository;
            _mapper = mapper;
        }

        public async Task AddProductColorAsync(ProductColorCreateDTO productColorCreateDTO)
        {
            ProductColor productColor = _mapper.Map<ProductColor>(productColorCreateDTO);

            await _productColorRepository.AddEntityAsync(productColor);
            await _productColorRepository.CommitAsync();
        }

        public void DeleteProductColor(int id)
        {
            var existProductColor = _productColorRepository.GetEntity(x => x.Id == id);
            if (existProductColor == null) throw new EntityNotFoundException("ProductColor tapilmadi");

            _productColorRepository.DeleteEntity(existProductColor);
            _productColorRepository.Commit();
        }

        public List<ProductColorGetDTO> GetAllProductColors(Func<ProductColor, bool>? func = null)
        {
            var productColors = _productColorRepository.GetAllEntities(func);
            List<ProductColorGetDTO> productColorDTO = _mapper.Map<List<ProductColorGetDTO>>(productColors);


            return productColorDTO;
        }

        public ProductColorGetDTO GetProductColor(Func<ProductColor, bool>? func = null)
        {
            var productColor = _productColorRepository.GetEntity(func);
            ProductColorGetDTO productColorDTO = _mapper.Map<ProductColorGetDTO>(productColor);


            return productColorDTO;
        }

        public void ReturnProductColor(int id)
        {
            var existProductColor = _productColorRepository.GetEntity(x => x.Id == id);
            if (existProductColor == null) throw new EntityNotFoundException("ProductColor tapilmadi");

            _productColorRepository.ReturnEntity(existProductColor);

            _productColorRepository.Commit();
        }

        public void SoftDelete(int id)
        {
            var existProductColor = _productColorRepository.GetEntity(x => x.Id == id);
            if (existProductColor == null) throw new EntityNotFoundException("ProductColor tapilmadi");
            existProductColor.DeletedDate = DateTime.UtcNow.AddHours(4);

            _productColorRepository.SoftDelete(existProductColor);

            _productColorRepository.Commit();
        }

        public void UpdateProductColor(ProductColorUpdateDTO updateDTO)
        {
            var oldProductColor = _productColorRepository.GetEntity(x => x.Id == updateDTO.Id);
            if (oldProductColor == null) throw new EntityNotFoundException("ProductColor tapilmadi");

            oldProductColor.UpdatedDate = DateTime.UtcNow.AddHours(4);

            oldProductColor.HexCode = updateDTO.HexCode;
            oldProductColor.Name = updateDTO.Name;

            _productColorRepository.Commit();
        }
    }
}
