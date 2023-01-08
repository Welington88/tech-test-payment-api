using System;
namespace ApiPayment.Domain.Repositories.Base
{
	public interface IBaseRepository<T>
	{
        public T _service { get; set; }
    }
}

