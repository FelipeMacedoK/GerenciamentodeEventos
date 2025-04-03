using GerenciamentodeEventos.Model;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentodeEventos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Evento> Evento { get; set; }
        public DbSet<Participante> Participante { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Organizador> Organizador { get; set; }
    }
}