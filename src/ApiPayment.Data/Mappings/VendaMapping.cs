using ApiPayment.Data.Extensions;
using ApiPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPayment.Data.Mappings;

public class VendaMapping : EntityTypeConfiguration<Venda>
{
    public override void Map(EntityTypeBuilder<Venda> builder)
    {
        //Mapeaia a chave primaria
        builder.HasKey(v => new { v.NumeroPedido});

    }

}