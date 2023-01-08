using ApiPayment.Data.Extensions;
using ApiPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPayment.Data.Mappings;

public class VendedorMapping : EntityTypeConfiguration<Vendedor>
{
    public override void Map(EntityTypeBuilder<Vendedor> builder)
    {
        //Mapeaia a chave primaria
        builder.HasKey(v => new { v.Id });
        builder.HasIndex(v => new { v.Cpf }).IsUnique();

    }
}