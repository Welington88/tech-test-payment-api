using System;
namespace ApiPayment.CC.Dto.DTOs;

public class VendedorDTO
{
    public int Id { get; set; }

    public string? Cpf { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }
}

