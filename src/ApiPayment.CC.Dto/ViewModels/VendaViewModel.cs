using System;
using ApiPayment.CC.Dto.Enums;

namespace ApiPayment.CC.Dto.ViewModels;

public class VendaViewModel 
{
    public int NumeroPedido { get; set; }

    public DateTime Data { get; set; }

    public VendedorViewModel Vendedor { get; set; }

    public List<ItensVenda> Itens { get; set; }

    public Status Status { get; set; }

    public class ItensVenda
    {
        public string Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }
    }
}

