using System;
using ApiPayment.CC.Dto.Enums;
using ApiPayment.Domain.Entities.Base;

namespace ApiPayment.Domain.Entities;

public class Venda : Entity<Venda>
{
    public int NumeroPedido { get; set; }

    public DateTime Data { get; set; }

    public int Vendedor { get; set; }

    public string Itens { get; set; }

    public Status Status { get; set; }

    public class ItensVenda
    {
        public string Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }
    }
}
