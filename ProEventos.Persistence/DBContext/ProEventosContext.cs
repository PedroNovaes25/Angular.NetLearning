using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.DBContext
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) 
            : base (options){}
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(Pe => new { Pe.EventoId, Pe.PalestranteId });

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                .HasMany(p => p.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
