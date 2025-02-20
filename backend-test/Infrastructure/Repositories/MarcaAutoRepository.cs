using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MarcaAutoRepository : GenericRepository<MarcaAuto>, IMarcaAutoRepository
    {
        private readonly AppDbContext _context;
        public MarcaAutoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MarcaAuto>> GetMarcaAutosByNombre(string nombre)
        {
            return await _context.MarcaAutos.Where(m => m.Nombre.Contains(nombre)).ToListAsync();
        }
    }
}
