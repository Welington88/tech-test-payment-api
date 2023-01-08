using ApiPayment.Application.Interfaces;
using ApiPayment.Application.ServiceManagement;
using ApiPayment.Application.Services;
using ApiPayment.Data.Contexts;
using ApiPayment.Data.Repositories;
using ApiPayment.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ApiPayment.Test.DataBase;

public class TestNativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        // ASPNET
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IUrlHelper>(factory =>
        {
            var actionContext = factory.GetService<IActionContextAccessor>()
                                       .ActionContext;
            return new UrlHelper(actionContext);
        });

        //Injeção dos serviços
        services.AddScoped<IVendaService, VendaService>();
        services.AddScoped<IVendedorService, VendedorService>();

        //Injeção dos repositórios
        services.AddScoped<IVendaRepository, VendaRepository>();
        services.AddScoped<IVendedorRepository, VendedorRepository>();

        //Injeção do contexto
        services.AddScoped<Context>();
        services.AddDbContext<Context>();

        //Injeção do controle de Serviços
        services.AddSingleton<AsyncService>();
    }
}