using Microsoft.EntityFrameworkCore;
using ValidaArquivo.Domain.Entities;

namespace ValidaArquivo.Data
{
    /// <summary>
    /// Classe de contexto do Entity Framework
    /// - Mapeia as entidades para tabelas no banco de dados
    /// </summary>
    public class ValidaArquivoContext : DbContext
    {
        public ValidaArquivoContext(DbContextOptions<ValidaArquivoContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ArquivoUpload> ArquivoUploads { get; set; }
        public DbSet<AnaliseArquivo> AnaliseArquivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArquivoUpload>()
                .HasMany(a => a.AnaliseArquivos)
                .WithOne(a => a.ArquivoUpload)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <summary>
        /// Personaliza o método de confirmação transação, adicionando as datas de alteração e criação do objeto
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseObject && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseObject)entityEntry.Entity).DataAlteracao = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((BaseObject)entityEntry.Entity).DataCriacao = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}
