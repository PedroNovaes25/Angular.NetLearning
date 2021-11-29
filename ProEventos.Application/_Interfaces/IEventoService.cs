using ProEventos.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(EventoDto eventoModel, bool includePalestrante);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto eventoModel);
        Task<bool> DeleteEvento(int eventoId);

        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrante = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante);
        Task<EventoDto> GetEventosByIdAsync(int eventoId, bool includePalestrante);
    }
}
