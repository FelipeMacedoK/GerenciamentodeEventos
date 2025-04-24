using GerenciamentodeEventos.Model;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentodeEventos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Inscricao> Inscricao { get; set; }
        public DbSet<Local> Local { get; set; }
        public DbSet<Organizador> Organizador { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
    }
} 