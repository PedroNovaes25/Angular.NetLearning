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
    public class PalestrantesPersist : IPalestrantesPersist
    {
        private readonly ProEventosContext _context;

        public PalestrantesPersist(ProEventosContext context)
        {
            this._context = context;
        }

        #region Palestrantes

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            //using (var ctx = _context)
            //{
                IQueryable<Palestrante> query = _context.Palestrantes
                    .Include(p => p.RedesSociais);

                if (includeEventos)
                    query = query.Include(p => p.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);

                query = query.OrderBy(p => p.Id);

                return await query.ToArrayAsync();
            //}
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            //using (var ctx = _context)
            //{
                IQueryable<Palestrante> query = _context.Palestrantes
                    .Include(p => p.RedesSociais);

                if (includeEventos)
                    query = query.Include(p => p.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);

                query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToUpper().Contains(nome.ToUpper()));

                return await query.ToArrayAsync();
            //}
        }

        public async Task<Palestrante> GetPalestrantesByIdAsync(int palestranteId, bool includeEventos)
        {
            //using (var ctx = _context)
            //{
                IQueryable<Palestrante> query = _context.Palestrantes
                    .Include(p => p.RedesSociais);

                if (includeEventos)
                    query = query.Include(p => p.PalestranteEvento)
                        .ThenInclude(pe => pe.Evento);

                query = query.OrderBy(p => p.Id)
                    .Where(e => e.Id == palestranteId);

                return await query.FirstOrDefaultAsync();
            //}
        }

        #endregion
    }
}
