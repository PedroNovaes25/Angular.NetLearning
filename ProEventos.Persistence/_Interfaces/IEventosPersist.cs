using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrante = false);
        Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrante = false);
        Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrante = false);
    }
}
