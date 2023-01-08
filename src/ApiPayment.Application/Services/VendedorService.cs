using System;
using ApiPayment.Application.Interfaces;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Domain.DTOs;
using ApiPayment.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiPayment.Application.Services;

public class VendedorService : IVendedorService
{
    private readonly IConfiguration _configuration;
    private readonly IVendedorRepository _repository;
    private readonly ILogger<VendaService> _logger;

    public VendedorService(IConfiguration configuration, IVendedorRepository repository, ILogger<VendaService> logger)
    {
        _configuration = configuration;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<VendedorViewModel>> Get()
    {
        try
        {
            var listaVendedores = await _repository.Get();
            return listaVendedores;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }   
    }

    public async Task<VendedorViewModel> GetVendedorId(int NumeroVendedor)
    {
        try
        {
            var listaVendedores = await _repository.GetVendedorId(NumeroVendedor);
            return listaVendedores;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PostVendedor(VendedorDTO vendaServiceDTO)
    {
        try
        {
            var vendedor = await _repository.PostVendedor(vendaServiceDTO);
            return vendedor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PutVendedor(int NumeroVendedor, VendedorDTO vendaServiceDTO)
    {
        try
        {
            var vendedor = await _repository.PutVendedor(NumeroVendedor, vendaServiceDTO);
            return vendedor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteVendedor(int NumeroVendedor)
    {
        try
        {
            var vendedor = await _repository.DeleteVendedor(NumeroVendedor);
            return vendedor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception(ex.Message);
        }
    }
}

