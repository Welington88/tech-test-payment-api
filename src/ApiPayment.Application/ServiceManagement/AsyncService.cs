using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ApiPayment.Application.ServiceManagement;

public class AsyncService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public AsyncService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Execute<T>(Action<T> bullet, Action<Exception> handler = null)
    {
        Task.Run(() =>
        {
            using var scope = _scopeFactory.CreateScope();
            var dependency = scope.ServiceProvider.GetRequiredService<T>();
            try
            {
                bullet(dependency);
            }
            catch (Exception e)
            {
                handler?.Invoke(e);
            }
            finally
            {
                dependency = default;
            }
        });
    }

    public void ExecuteAsync<T>(Func<T, Task> bullet, Action<Exception> handler = null)
    {
        Task.Run(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var dependency = scope.ServiceProvider.GetRequiredService<T>();
            try
            {
                await bullet(dependency);
            }
            catch (Exception e)
            {
                handler?.Invoke(e);
            }
            finally
            {
                dependency = default;
            }
        });
    }
}

