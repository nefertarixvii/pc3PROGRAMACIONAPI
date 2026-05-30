using Microsoft.EntityFrameworkCore;
using ApiInteligenteTareas.Models;

namespace ApiInteligenteTareas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }
    }
}