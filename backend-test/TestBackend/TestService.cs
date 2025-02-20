using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Application.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Application.DTOs;
using Infrastructure.Repositories;
using AutoMapper;

namespace TestBackend
{
    public class TestService : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly MarcaAutoService _service;

        public TestService()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MarcaAuto, MarcaAutoDto>();
                cfg.CreateMap<MarcaAutoDto, MarcaAuto>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            var repository = new MarcaAutoRepository(_context);
            _service = new MarcaAutoService(repository, mapper);

            SeedData();
        }

        private void SeedData()
        {
            _context.MarcaAutos.Add(new MarcaAuto { Id = 1, Nombre = "Toyota", PaisOrigen = "Japón" });
            _context.MarcaAutos.Add(new MarcaAuto { Id = 2, Nombre = "Honda", PaisOrigen = "Japón" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task AddTest()
        {
            var newMarca = new MarcaAutoDto { Id = 3, Nombre = "Nissan", PaisOrigen = "Japón" };
            await _service.Add(newMarca);

            var marca = await _context.MarcaAutos.FindAsync(3);
            Assert.NotNull(marca);
            Assert.Equal("Nissan", marca.Nombre);
        }

        [Fact]
        public async Task UpdateTest()
        {
            var updateMarca = new MarcaAutoDto { Id = 1, Nombre = "Toyota Updated", PaisOrigen = "Japón" };
            await _service.Update(updateMarca);

            var marca = await _context.MarcaAutos.FindAsync(1);
            Assert.NotNull(marca);
            Assert.Equal("Toyota Updated", marca.Nombre);
        }

        [Fact]
        public async Task DeleteTest()
        {
            await _service.Delete(2);

            var marca = await _context.MarcaAutos.FindAsync(2);
            Assert.Null(marca);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            var marca = await _service.GetById(1);

            Assert.NotNull(marca);
            Assert.Equal("Toyota", marca.Nombre);
        }

        [Fact]
        public async Task GetAllTest()
        {
            var marcas = await _service.GetAll();

            Assert.Equal(2, marcas.Count());
        }

        [Fact]
        public async Task GetMarcaAutosByNombreTest()
        {
            var marcas = await _service.GetMarcaAutosByNombre("Toyota");

            Assert.Single(marcas);
            Assert.Equal("Toyota", marcas.First().Nombre);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
