using System;
using ApiPayment.Application.Interfaces.Base;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.ViewModels;

namespace ApiPayment.Application.Interfaces;

public interface IVendedorService : IBaseAppService
{
    Task<List<VendedorViewModel>> Get();

    Task<VendedorViewModel> GetVendedorId(int NumeroVendedor);

    Task<bool> PostVendedor(VendedorDTO vendaServiceDTO);

    Task<bool> PutVendedor(int NumeroVendedor, VendedorDTO vendaServiceDTO);

    Task<bool> DeleteVendedor(int NumeroVendedor);
}

