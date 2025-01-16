using Esterdigi.Core.Lib.Constants;
using Alumisoft.Pagamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumisoft.Pagamento.Infrastructure.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            //Entity
            entity.ToTable("alumisoft_clientes");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.Email }).IsUnique();

            //Properties
            entity.Property(x => x.Nome).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);

            entity.Property(x => x.Password).IsRequired().HasMaxLength(128).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(160).HasColumnType(Constants.Varchar);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTime);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTime);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

        }
    }
}