using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.DBContext;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Storage
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoPersist(ProEventosContext context)
        {
            this._context = context;
        }

        #region Eventos
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            //using (var ctx = _context)
            //{
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lote)
                    .Include(e => e.RedesSociais);

                if (includePalestrante)
                    query = query.Include(e => e.EventoPalestrante)
                        .ThenInclude(ep => ep.Palestrante);
                
                query = query.AsNoTracking().OrderBy(e => e.EventoCodigo);

                return await query.ToArrayAsync();
            //}
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            //using (var ctx = _context)
            //{
                IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lote)
                    .Include(e => e.RedesSociais);

                if (includePalestrante)
                    query = query.Include(e => e.EventoPalestrante)
                        .ThenInclude(ep => ep.Palestrante);
                
                query = query.AsNoTracking().OrderBy(e => e.EventoCodigo)
                    .Where(e => e.Tema.ToLower()
                    .Contains(tema.ToLower()));

                return await query.ToArrayAsync();
            //}
        }

        public async Task<Evento> GetEventosByIdAsync(int eventoId, bool includePalestrante = false)
        {
            //using (var ctx = _context)
            //{
                IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lote)
                    .Include(e => e.RedesSociais);

                if (includePalestrante)
                    query = query.Include(e => e.EventoPalestrante)
                        .ThenInclude(ep => ep.Palestrante);

                query = query.AsNoTracking().OrderBy(e => e.EventoCodigo)
                    .Where(e => e.EventoCodigo == eventoId);

                return await query.FirstOrDefaultAsync();
            //}
        }

        #endregion
    }
}
