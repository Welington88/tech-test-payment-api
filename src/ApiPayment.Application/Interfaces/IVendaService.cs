using System;
using ApiPayment.Application.Interfaces.Base;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.Enums;
using ApiPayment.CC.Dto.ViewModels;

namespace ApiPayment.Application.Interfaces;

public interface IVendaService : IBaseAppService
{
    Task<List<VendaViewModel>> Get();

    Task<VendaViewModel> GetVendaId(int NumeroVenda);

    Task<bool> PostVenda(VendaDTO vendaServiceDTO);

    Task<bool> PutVenda(int NumeroVenda, VendaDTO vendaServiceDTO, Status lastStatus);

    Task<bool> DeleteVenda(int NumeroVenda);
}

