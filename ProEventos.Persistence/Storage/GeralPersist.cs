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
    public class GeralPersist : IGeralPersist
    {
        private readonly ProEventosContext _context;

        public GeralPersist(ProEventosContext context)
        {
            this._context = context;
        }

        #region Geral
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);

            //using (var ctx =  _context)
            //{
            //    ctx.Add(entity);
            //}
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);

            //using (var ctx = _context)
            //{
            //    ctx.Update(entity);
            //}
        }

        public void Delete<T>(T entity) where T : class
        {

            _context.Remove(entity);
            //using (var ctx = _context)
            //{
            //    ctx.Remove(entity);
            //}
        }

        public void DeleteRange<T>(T[] entitys) where T : class
        {

            _context.RemoveRange(entitys);

            //using (var ctx = _context)
            //{
            //    ctx.RemoveRange(entitys);
            //}
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;

            //using (var ctx = _context)
            //{
            //    return (await ctx.SaveChangesAsync()) > 0;
            //}
        }

        #endregion

    }
}
