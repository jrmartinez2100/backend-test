using Domain.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No se encontró la entidad con ID {id}");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No se encontró la entidad con ID {id}");

            return entity;
        }

        public async Task Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
