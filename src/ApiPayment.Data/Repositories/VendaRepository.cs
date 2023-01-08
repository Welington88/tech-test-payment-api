using System;
using System.Linq;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.Enums;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Data.Contexts;
using ApiPayment.Domain.DTOs;
using ApiPayment.Domain.Entities;
using ApiPayment.Domain.Repositories;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiPayment.Data.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly ILogger<VendaRepository> _logger;
    private readonly Context _context;

    public VendaRepository(ILogger<VendaRepository> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<VendaViewModel>> Get()
    {
        try
        {
            var listaVendas = await _context.Vendas.ToListAsync();
            var listaVendedores = await _context.Vendedores.ToListAsync();
            var listaVendasViewModel = VendaTransformation.GetViewModel(listaVendas,listaVendedores);
            return listaVendasViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VendaViewModel> GetVendaId(int numeroPedido)
    {
        try
        {
            var listaVendas = await _context.Vendas.ToListAsync();
            var listaVendedores = await _context.Vendedores.ToListAsync();
            var vendaConsulta = listaVendas.Where<Venda>(v => v.NumeroPedido == numeroPedido).FirstOrDefault();
            var vendedor = listaVendedores.Where<Vendedor>(v => v.Id == vendaConsulta.Vendedor).FirstOrDefault();
            var vendasViewModel = VendaTransformation.GetViewModel(vendaConsulta,vendedor);
            return vendasViewModel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PostVenda(VendaDTO vendaDTO)
    {
        try
        {
            var listaVendasBancoDeDados = await _context.Vendas.ToListAsync();
            var listaVendedores = await _context.Vendedores.ToListAsync();
            var vendasDomain = VendaTransformation.GetDomain(vendaDTO);
            vendasDomain.Status = Status.AguardandoPagamento;
            var vendedorSelecionado = listaVendedores.Where<Vendedor>(v => v.Cpf == vendaDTO.Vendedor.Cpf).FirstOrDefault();
            if (vendedorSelecionado is null)
            {
                var vendedor = new Vendedor() {
                    Cpf = vendaDTO.Vendedor.Cpf,
                    Email = vendaDTO.Vendedor.Email,
                    Id = vendaDTO.Vendedor.Id,
                    Nome = vendaDTO.Vendedor.Nome,
                    Telefone = vendaDTO.Vendedor.Telefone
                };

                await _context.AddAsync(vendedor);
            }
            var vendedorId = vendedorSelecionado is null ? listaVendedores.Max(v => v.Id): vendedorSelecionado.Id;
            vendasDomain.Vendedor = ++vendedorId;
            await _context.Vendas.AddAsync(vendasDomain);
            var result = _context.SaveChangesAsync();

            if (result is null)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> PutVenda(int numeroPedido, VendaDTO vendaDTO, Status lastStatus)
    {
        try
        {
            var vendaBase = GetVendaId(numeroPedido);
            if (vendaBase is null || vendaDTO.NumeroPedido != numeroPedido)
            {
                return false;
            }
            
            var vendaUpdate = VendaTransformation.GetDomain(vendaDTO);
            _context.ChangeTracker.Clear();
            _context.Vendas.Update(vendaUpdate);
            var result = await _context.SaveChangesAsync();
            return true;
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteVenda(int numeroPedido)
    {
        try
        {
            var listaVendas = await _context.Vendas.ToListAsync();
            var vendaBase = listaVendas.Where<Venda>(v => v.NumeroPedido == numeroPedido).FirstOrDefault();
            if (vendaBase is null || vendaBase.NumeroPedido != numeroPedido)
            {
                return false;
            }

            _context.Vendas.Remove(vendaBase);
            var result = await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

