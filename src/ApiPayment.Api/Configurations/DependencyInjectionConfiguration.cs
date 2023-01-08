using System;
using ApiPayment.CC.Ioc;

namespace ApiPayment.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDIConfiguration(this IServiceCollection services)
    {
        NativeInjectorBootStrapper.RegisterServices(services);
    }
}

