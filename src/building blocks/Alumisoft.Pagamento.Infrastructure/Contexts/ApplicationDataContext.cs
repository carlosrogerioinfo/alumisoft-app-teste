using Microsoft.EntityFrameworkCore;
using Alumisoft.Pagamento.Domain.Entities;
using Alumisoft.Pagamento.Infrastructure.Mappings;

namespace Alumisoft.Pagamento.Infrastructure.Contexts
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext() { }

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PagamentoCliente> PagamentosClientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=appdbtest.mssql.somee.com;Database=appdbtest;User ID=carlos_mdb_SQLLogin_1;Password=wxgrh515lh;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new PagamentoClienteMap());
        }
    }
}