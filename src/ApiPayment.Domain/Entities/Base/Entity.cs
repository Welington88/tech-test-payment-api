using System;
namespace ApiPayment.Domain.Entities.Base;

public abstract class Entity<T> where T : Entity<T>
{
    protected Entity()
    {
    }
}

