using Microsoft.EntityFrameworkCore;
using ProEventos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base (options)
        {
        }
        DbSet<Evento> Eventos { get; set; }
    }
}
