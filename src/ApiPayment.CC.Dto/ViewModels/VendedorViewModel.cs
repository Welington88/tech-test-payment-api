using System;
namespace ApiPayment.CC.Dto.ViewModels;

public class VendedorViewModel
{
    public int Id { get; set; }

    public string? Cpf { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }
}

