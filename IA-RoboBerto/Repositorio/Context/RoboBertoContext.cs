using IA_RoboBerto.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IA_RoboBerto.Repositorio.Context
{
    public class RoboBertoContext : DbContext
    {
        public DbSet<Role> Role { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Chamado> Chamado { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
        public RoboBertoContext(DbContextOptions<RoboBertoContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-----------
            var role = modelBuilder.Entity<Role>();
            role.ToTable("role");
            role.HasKey(r => r.Id);
            role.Property(r => r.Id).HasColumnName("role_codigo").IsRequired();
            role.Property(r => r.Nome).HasColumnName("role_name").HasMaxLength(50).IsRequired();

            //-----------
            var departamento = modelBuilder.Entity<Departamento>();
            departamento.ToTable("departamento");
            departamento.HasKey(d => d.Id);
            departamento.Property(d => d.Id).HasColumnName("dep_codigo").IsRequired();
            departamento.Property(d => d.Nome).HasColumnName("dep_nome").HasMaxLength(255).IsRequired();

            //-----------
            var usuario = modelBuilder.Entity<Usuario>();
            usuario.ToTable("usuario");
            usuario.HasKey(u => u.Id);
            usuario.Property(u => u.Id).HasColumnName("usu_codigo").IsRequired();
            usuario.Property(u => u.Nome).HasColumnName("usu_nome").HasMaxLength(255).IsRequired();
            usuario.Property(u => u.Email).HasColumnName("usu_email").HasMaxLength(255).IsRequired();
            usuario.Property(u => u.SenhaHash).HasColumnName("usu_senha_hash").HasMaxLength(255).IsRequired();
            usuario.Property(u => u.Telefone).HasColumnName("usu_telefone").HasMaxLength(255).IsRequired();
            usuario.Property(u => u.DataCriacao).HasColumnName("usu_datacriacao").IsRequired();


            // FK -> departamento (USUARIO.dep_codigo)
            usuario.HasOne(u => u.Departamento)
                   .WithMany(d => d.Usuarios)
                   .HasForeignKey("dep_codigo")
                   .OnDelete(DeleteBehavior.SetNull);

            usuario.HasMany(u => u.Roles)
             .WithMany()
             .UsingEntity<Dictionary<string, object>>
             ("role_usuario", j => j.HasOne<Role>().WithMany().HasForeignKey("role_codigo").HasConstraintName("FK_role_usuario_role").OnDelete(DeleteBehavior.Cascade),
                  j => j.HasOne<Usuario>().WithMany().HasForeignKey("usu_codigo").HasConstraintName("FK_role_usuario_usuario").OnDelete(DeleteBehavior.Cascade),
                  j => j.HasKey("usu_codigo", "role_codigo")  // chave primária composta
             );


            //-----------
            var categoria = modelBuilder.Entity<Categoria>();
            categoria.ToTable("categoria");
            categoria.HasKey(c => c.Id);
            categoria.Property(c => c.Id).HasColumnName("cat_codigo").IsRequired();
            categoria.Property(c => c.Nome).HasColumnName("cat_nome").HasMaxLength(255).IsRequired();

            categoria.HasMany(c => c.Chamados)
                     .WithOne(ch => ch.Categoria)
                     .HasForeignKey("cat_codigo")
                     .OnDelete(DeleteBehavior.Cascade);

            //-----------
            var chamado = modelBuilder.Entity<Chamado>();
            chamado.ToTable("chamado");
            chamado.HasKey(c => c.Id);
            chamado.Property(c => c.Id).HasColumnName("cha_codigo").IsRequired();

            chamado.Property(c => c.Titulo).HasColumnName("cha_titulo").HasMaxLength(255).IsRequired();
            chamado.Property(c => c.Status).HasColumnName("cha_status").IsRequired();
            chamado.Property(c => c.SugestaoGemini).HasColumnName("cha_sugestao").HasColumnType("text").IsRequired();
            chamado.Property(c => c.Descricao).HasColumnName("cha_descricao").IsRequired();
            chamado.Property(c => c.DataAbertura).HasColumnName("cha_aberto_em").IsRequired();
            chamado.Property(c => c.DataFechamento).HasColumnName("cha_resolvido_em");
            chamado.Property(c => c.SugestaoResolveu).HasColumnName("cha_resolvido_com_ia");
            chamado.Property(c => c.Prioridade).HasColumnName("cha_prioridade").IsRequired();
            chamado.Property(c => c.SlaVenceEm).HasColumnName("cha_sla_vence_em").IsRequired();

            // Autor (required)
            chamado.HasOne(c => c.Autor)
                   .WithMany() // se Usuario não tem coleção de chamados, usar sem coleção inversa
                   .HasForeignKey("cha_usuario_autor")
                   .OnDelete(DeleteBehavior.Cascade);

            // Tecnico (optional)
            chamado.HasOne(c => c.Tecnico)
                   .WithMany()
                   .HasForeignKey("cha_usuario_tecnico")
                   .OnDelete(DeleteBehavior.SetNull);

            // Chamado -> Mensagens
            chamado.HasMany(c => c.Mensagens)
                   .WithOne() // Mensagem pode não ter propriedade Chamado; iremos mapear FK abaixo
                   .HasForeignKey("cha_codigo")
                   .OnDelete(DeleteBehavior.Cascade);

            //-----------
            var mensagem = modelBuilder.Entity<Mensagem>();
            mensagem.ToTable("mensagem");
            mensagem.HasKey(m => m.Id);
            mensagem.Property(m => m.Id).HasColumnName("men_codigo").IsRequired();
            mensagem.Property(m => m.Texto).HasColumnName("men_texto").HasColumnType("text").IsRequired();
            mensagem.Property(m => m.DataHoraMensagem).HasColumnName("men_data_hora_texto").IsRequired();

            // FK mensagem -> chamado (cha_codigo) (usando shadow FK caso Mensagem não tenha Chamado nav)
            mensagem.HasOne<Chamado>()
                     .WithMany(c => c.Mensagens)
                     .HasForeignKey("cha_codigo")
                     .OnDelete(DeleteBehavior.Cascade);

            // FK mensagem -> usuario autor (usu_codigo)
            mensagem.HasOne(m => m.Autor)
                     .WithMany()
                     .HasForeignKey("usu_codigo")
                     .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
