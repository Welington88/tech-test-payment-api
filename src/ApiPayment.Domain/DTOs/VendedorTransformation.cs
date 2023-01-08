using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Domain.Entities;

namespace ApiPayment.Domain.DTOs;

public static class VendedorTransformation
{
    public static Vendedor GetDomain(VendedorDTO DTO)
    {
        var domain = new Vendedor() {
            Cpf = DTO.Cpf,
            Email = DTO.Email,
            Id = DTO.Id,
            Nome = DTO.Nome,
            Telefone = DTO.Telefone
        };
        return domain;
    }

    public static List<Vendedor> GetDomain(List<VendedorDTO> listDTO)
    {

        var listDTORetorno = new List<Vendedor>();

        foreach (var DTO in listDTO)
        {
            var domain = new Vendedor()
            {
                Cpf = DTO.Cpf,
                Email = DTO.Email,
                Id = DTO.Id,
                Nome = DTO.Nome,
                Telefone = DTO.Telefone
            };
            listDTORetorno.Add(domain);
        }
        
        return listDTORetorno;
    }

    public static VendedorViewModel GetViewModel(Vendedor domain)
    {
        var viewModel = new VendedorViewModel() {
            Cpf = domain.Cpf,
            Email = domain.Email,
            Id= domain.Id,
            Nome= domain.Nome,
            Telefone = domain.Telefone
        };

        return viewModel;
    }

    public static List<VendedorViewModel> GetViewModel(List<Vendedor> listDomain)
    {
        var listViewModel = new List<VendedorViewModel>();

        foreach (var domain in listDomain)
        {
            var viewModel = new VendedorViewModel()
            {
                Cpf = domain.Cpf,
                Email = domain.Email,
                Id = domain.Id,
                Nome = domain.Nome,
                Telefone = domain.Telefone
            };

            listViewModel.Add(viewModel);
        }
        

        return listViewModel;
    }
}