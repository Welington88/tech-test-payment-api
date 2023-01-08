using ApiPayment.Data.Extensions;
using ApiPayment.Data.Mappings;
using ApiPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPayment.Data.Contexts;

public class Context : DbContext
{
		public Context(DbContextOptions<Context> options) : base(options) 
		{
		}

		public DbSet<Venda> Vendas { get; set; }
		public DbSet<Vendedor> Vendedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddConfiguration(new VendaMapping());
        modelBuilder.AddConfiguration(new VendedorMapping());

        modelBuilder.Entity<Venda>().Property(v => v.Data).IsRequired();
        modelBuilder.Entity<Venda>().Property(v => v.Vendedor).IsRequired();
        modelBuilder.Entity<Venda>().Property(v => v.Status).IsRequired();
        modelBuilder.Entity<Venda>().Property(v => v.Itens).IsRequired();

        modelBuilder.Entity<Venda.ItensVenda>().HasKey(i => i.Produto);
        modelBuilder.Entity<Venda.ItensVenda>().Property(i => i.Produto).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Venda.ItensVenda>().Property(i => i.Quantidade).IsRequired();
        modelBuilder.Entity<Venda.ItensVenda>().Property(i => i.ValorUnitario).HasPrecision(2).IsRequired();

        modelBuilder.Entity<Vendedor>().Property(v => v.Cpf).HasMaxLength(20).IsRequired();
        modelBuilder.Entity<Vendedor>().Property(v => v.Nome).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Vendedor>().Property(v => v.Email).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Vendedor>().Property(v => v.Telefone).HasMaxLength(20).IsRequired();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}