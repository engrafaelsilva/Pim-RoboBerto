using IA_RoboBerto.Modelos;
using Microsoft.EntityFrameworkCore;

namespace IA_RoboBerto.Repositorio.Context
{
    public class RoboBertoContext : DbContext
    {

        public RoboBertoContext(DbContextOptions<RoboBertoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var role = modelBuilder.Entity<Role>();
            role.ToTable("role");                  // nome da tabela em minúsculo
            role.HasKey(r => r.Id);                // chave primária
            role.Property(r => r.Id)
                .HasColumnName("role_codigo")      // mapeia Id → role_codigo
                .IsRequired();

            role.Property(r => r.Name)
                .HasColumnName("role_name")        // mapeia Name → role_name
                .HasMaxLength(50)
                .IsRequired();

        }
        public DbSet<Role> Roles { get; set; }
    }
}
