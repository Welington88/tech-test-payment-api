using System.Text.Json;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Domain.Entities;

namespace ApiPayment.Domain.DTOs;

public static class VendaTransformation
{

	public static Venda GetDomain(VendaDTO DTO) {

		var itemVendaDTO = new List<VendaDTO.ItensVenda>() {
			new VendaDTO.ItensVenda(){
				Produto = "Produto",
				Quantidade = int.MinValue,
				ValorUnitario = decimal.MinValue
			}
		};

		var itemVendaDefault = DTO.Itens is null ? itemVendaDTO : DTO.Itens;

		var itensVenda = new List<Venda.ItensVenda>();

		foreach (var item in DTO.Itens)
		{
			var itemListaVenda = new Venda.ItensVenda() {
				Produto = item.Produto,
				Quantidade = item.Quantidade,
				ValorUnitario = item.ValorUnitario
			};
			itensVenda.Add(itemListaVenda);
		}

		var vendedorDomain = new Vendedor() {
			Cpf = DTO.Vendedor.Cpf,
			Email = DTO.Vendedor.Email,
			Id = DTO.Vendedor.Id,
			Nome = DTO.Vendedor.Nome,
			Telefone = DTO.Vendedor.Telefone
		};

        var jsonItensVenda = JsonSerializer.Serialize(itensVenda);

        Venda domain = new Venda() {

            NumeroPedido = DTO.NumeroPedido,
			Data = DTO.Data,
			Itens = jsonItensVenda,
			Status = DTO.Status,
			Vendedor = vendedorDomain.Id
		};

		return domain;
	}

    public static List<Venda> GetDomain(List<VendaDTO> listDTO)
    {

		var listDomain = new List<Venda>();

        foreach (var DTO in listDTO)
		{
            var itemVendaDTO = new List<VendaDTO.ItensVenda>() {
				new VendaDTO.ItensVenda(){
					Produto = "Produto",
					Quantidade = int.MinValue,
					ValorUnitario = decimal.MinValue
				}
			};

            var itemVendaDefault = DTO.Itens is null ? itemVendaDTO : DTO.Itens;

            var itensVenda = new List<Venda.ItensVenda>();

            foreach (var item in DTO.Itens)
            {
                var itemListaVenda = new Venda.ItensVenda()
                {
                    Produto = item.Produto,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario
                };
                itensVenda.Add(itemListaVenda);
            }

            var vendedorDomain = new Vendedor()
            {
                Cpf = DTO.Vendedor.Cpf,
                Email = DTO.Vendedor.Email,
                Id = DTO.Vendedor.Id,
                Nome = DTO.Vendedor.Nome,
                Telefone = DTO.Vendedor.Telefone
            };

            var jsonItensVenda = JsonSerializer.Serialize(itensVenda);

            Venda domain = new Venda()
            {
				NumeroPedido = DTO.NumeroPedido,
                Data = DTO.Data,
                Itens = jsonItensVenda,
                Status = DTO.Status,
                Vendedor = vendedorDomain.Id
            };

			listDomain.Add(domain);
        }

        return listDomain;
    }

    public static VendaViewModel GetViewModel(Venda domain, Vendedor vendedor) {


        var vendedorViewModel = new VendedorViewModel() {
			Cpf = vendedor.Cpf,
			Email = vendedor.Email,
			Id = vendedor.Id,
			Nome = vendedor.Nome,
			Telefone = vendedor.Telefone
		};

		var listItensVenda = new List<VendaViewModel.ItensVenda>();

        var listaItens = JsonSerializer.Deserialize<List<Venda.ItensVenda>>(domain.Itens);
        
		foreach (var item in listaItens)
		{
			var itensVendaViewModel = new VendaViewModel.ItensVenda() {
				Produto = item.Produto,
				Quantidade = item.Quantidade,
				ValorUnitario = item.ValorUnitario
			};

			listItensVenda.Add(itensVendaViewModel);
        }

		var viewModel = new VendaViewModel() {
			NumeroPedido = domain.NumeroPedido,
			Data = domain.Data,
			Status = domain.Status,
			Vendedor = vendedorViewModel,
			Itens = listItensVenda
		};

		return viewModel;
	}

    public static List<VendaViewModel> GetViewModel(List<Venda> listDomain, List<Vendedor> vendedores)
    {
        var listViewModel = new List<VendaViewModel>();

        foreach (var domain in listDomain)
        {
            var vendedor = vendedores.Where<Vendedor>(v => v.Id == domain.Vendedor).FirstOrDefault();
            var vendedorViewModel = new VendedorViewModel()
            {
                Cpf = vendedor.Cpf,
                Email = vendedor.Email,
                Id = vendedor.Id,
                Nome = vendedor.Nome,
                Telefone = vendedor.Telefone
            };

            var listItensVenda = new List<VendaViewModel.ItensVenda>();

            var itensJson = JsonSerializer.Deserialize<List<Venda.ItensVenda>>(domain.Itens);

            foreach (var item in itensJson)
            {    
                var itensVendaViewModel = new VendaViewModel.ItensVenda()
                {
                    Produto = item.Produto,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario
                };

                listItensVenda.Add(itensVendaViewModel);
            }

            var viewModel = new VendaViewModel()
            {
                NumeroPedido = domain.NumeroPedido,
                Data = domain.Data,
                Status = domain.Status,
                Vendedor = vendedorViewModel,
                Itens = listItensVenda
            };

            listViewModel.Add(viewModel);
        }

        return listViewModel;
    }
}

