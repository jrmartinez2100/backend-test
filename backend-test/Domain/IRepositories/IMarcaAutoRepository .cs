using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IMarcaAutoRepository : IGenericRepository<MarcaAuto>
    {
        Task<IEnumerable<MarcaAuto>> GetMarcaAutosByNombre(string nombre);
    }
}
