using Microsoft.EntityFrameworkCore;

namespace IA_RoboBerto.Repositorio.Context
{
    public class SexoContext : DbContext
    {
        public SexoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
