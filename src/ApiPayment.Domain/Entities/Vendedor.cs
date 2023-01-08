using System;
using ApiPayment.Domain.Entities.Base;

namespace ApiPayment.Domain.Entities;

public class Vendedor : Entity<Vendedor>
{
    public int Id { get; set; }

    public string? Cpf { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }
}

