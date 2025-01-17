using Esterdigi.Core.Lib.Constants;
using Alumisoft.Pagamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alumisoft.Pagamento.Infrastructure.Mappings
{
    public class PagamentoClienteMap : IEntityTypeConfiguration<PagamentoCliente>
    {
        public void Configure(EntityTypeBuilder<PagamentoCliente> entity)
        {
            //Entity
            entity.ToTable("alumisoft_pagamentos_clientes");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.ClienteId).IsRequired().HasColumnType(Constants.Guid);

            entity.Property(x => x.Valor).IsRequired().HasColumnType(Constants.Double);
            entity.Property(x => x.Data).IsRequired().HasColumnType(Constants.DateTime);
            entity.Property(x => x.Status).IsRequired().HasColumnType(Constants.SmallInt);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTime);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTime);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

        }
    }
}