using System;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Data.Contexts;
using ApiPayment.Domain.DTOs;
using ApiPayment.Domain.Entities;
using ApiPayment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiPayment.Data.Repositories;

public class VendedorRepository : IVendedorRepository
{
    private readonly ILogger<VendedorRepository> _logger;
    private readonly Context _context;

    public VendedorRepository(ILogger<VendedorRepository> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<VendedorViewModel>> Get()
    {
        try
        {
            var listaVendedores = await _context.Vendedores.ToListAsync();
            var listaVendedoresViewModel = VendedorTransformation.GetViewModel(listaVendedores);
            return listaVendedoresViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VendedorViewModel> GetVendedorId(int NumeroVendedor)
    {
        try
        {
            var listaVendedores = await _context.Vendedores.ToListAsync();
            var vendedorSelecionado = listaVendedores.Where<Vendedor>(v => v.Id == NumeroVendedor).FirstOrDefault();
            var vendedoreViewModel = VendedorTransformation.GetViewModel(vendedorSelecionado);
            return vendedoreViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PostVendedor(VendedorDTO vendedorDTO)
    {
        try
        {
            var vendedorBanco = VendedorTransformation.GetDomain(vendedorDTO);
            var result = await _context.AddAsync(vendedorBanco);

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PutVendedor(int NumeroVendedor, VendedorDTO vendedorDTO)
    {
        try
        {
            var listaVendedores = await _context.Vendedores.ToListAsync();
            var vendedorBase = listaVendedores.Where<Vendedor>(v => v.Id == NumeroVendedor).FirstOrDefault();
            if (vendedorBase is null || vendedorDTO.Id != NumeroVendedor)
            {
                return false;
            }

            var vendedorUpdate = VendedorTransformation.GetDomain(vendedorDTO);

            _context.Vendedores.Update(vendedorUpdate);

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteVendedor(int NumeroVendedor)
    {
        try
        {
            var listaVendedores = await _context.Vendedores.ToListAsync();
            var vendedorBase = listaVendedores.Where<Vendedor>(v => v.Id == NumeroVendedor).FirstOrDefault();
            if (vendedorBase is null || vendedorBase.Id != NumeroVendedor)
            {
                return false;
            }

            _context.Vendedores.Remove(vendedorBase);

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);
        }
    }
}