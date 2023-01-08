using ApiPayment.Application.Interfaces;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.Enums;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiPayment.Application.Services;

public class VendaService : IVendaService
{

    private readonly IConfiguration _configuration;
    private readonly IVendaRepository _repository;
    private readonly ILogger<VendaService> _logger;

    public VendaService(IConfiguration configuration, IVendaRepository repository, ILogger<VendaService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<VendaViewModel>> Get()
    {
        try
        {
            var listaVendas = await _repository.Get();
            return listaVendas;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VendaViewModel> GetVendaId(int numeroVenda)
    {
        try
        {
            var listaVendas= await _repository.GetVendaId(numeroVenda);
            return listaVendas;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PostVenda(VendaDTO vendaServiceDTO)
    {
        try
        {
            if (vendaServiceDTO is null)
            {
                return false;
            }

             var venda = await _repository.PostVenda(vendaServiceDTO);
             return venda;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PutVenda(int NumeroVenda, VendaDTO vendaServiceDTO, Status lastStatus)
    {
        try
        {
            if (NumeroVenda <= 0 || vendaServiceDTO is null)
            {
                return false;
            }

            bool exect = false;

            if (lastStatus.Equals(Status.AguardandoPagamento)
                && (vendaServiceDTO.Status.Equals(Status.Cancelada) || vendaServiceDTO.Status.Equals(Status.PagamentoAprovado))
            )
            {
                exect = true;
            } else if (lastStatus.Equals(Status.PagamentoAprovado)
                && (vendaServiceDTO.Status.Equals(Status.EnviadoParaTransportadora) || vendaServiceDTO.Status.Equals(Status.Cancelada)))
            {
                exect = true;
            } else if ((lastStatus.Equals(Status.EnviadoParaTransportadora)
                && vendaServiceDTO.Status.Equals(Status.Entregue)))
            {
                exect = true;
            } else if (lastStatus.Equals(vendaServiceDTO.Status))
            {
                exect = true;
            }

            if (exect)
            {
                var venda = await _repository.PutVenda(NumeroVenda, vendaServiceDTO, lastStatus);
                return venda;
            }
            else
            {
                return false;
            }
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteVenda(int NumeroVenda)
    {
        try
        {
            var venda = await _repository.DeleteVenda(NumeroVenda);
            return venda;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}