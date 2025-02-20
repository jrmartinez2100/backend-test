using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IMarcaAutoService
    {
        Task<IEnumerable<MarcaAutoDto>> GetAll();
        Task<MarcaAutoDto> GetById(int id);
        Task Add(MarcaAutoDto entity);
        Task Update(MarcaAutoDto entity);
        Task Delete(int id);
        Task<IEnumerable<MarcaAutoDto>> GetMarcaAutosByNombre(string nombre);
    }
}
