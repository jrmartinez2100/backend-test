using Application.DTOs;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MarcaAutoService : IMarcaAutoService
    {
        private readonly IMarcaAutoRepository _repository;
        private readonly IMapper _mapper;

        public MarcaAutoService(IMarcaAutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Add(MarcaAutoDto entityDto)
        {
            var marca = _mapper.Map<MarcaAuto>(entityDto);
            await _repository.Add(marca);
        }
        public async Task Delete(int id)
        {
            var marca = await _repository.GetById(id) ?? throw new KeyNotFoundException("Marca no encontrada");
            await _repository.Delete(id);
        }
        public async Task<IEnumerable<MarcaAutoDto>> GetAll()
        {
            var marcas = await _repository.GetAll();
            return _mapper.Map<IEnumerable<MarcaAutoDto>>(marcas);
        }
        public async Task<MarcaAutoDto> GetById(int id)
        {
            var marca = await _repository.GetById(id);
            return _mapper.Map<MarcaAutoDto>(marca);
        }

        public async Task<IEnumerable<MarcaAutoDto>> GetMarcaAutosByNombre(string nombre)
        {
            var marcas = await _repository.GetMarcaAutosByNombre(nombre);
            return _mapper.Map<IEnumerable<MarcaAutoDto>>(marcas);
        }

        public async Task Update(MarcaAutoDto entity)
        {
            var marca = await _repository.GetById(entity.Id);
            if (marca == null)
            {
                throw new KeyNotFoundException("Marca no encontrada");
            }
            _mapper.Map(entity, marca);
            await _repository.Update(marca);
        }
    }
}
