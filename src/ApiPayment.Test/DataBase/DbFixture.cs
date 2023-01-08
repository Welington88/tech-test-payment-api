using System;
using ApiPayment.CC.Ioc;
using ApiPayment.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ApiPayment.Test.DataBase;

public class DbFixture
{
    public IConfiguration Configuration { get; }
    public ServiceProvider ServiceProvider { get; private set; }

    public DbFixture()
    {
    }

    public void Init(bool testRepository = false)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddDbContext<Context>(options =>
            {
                options.UseSqlite("DataSource=:memory:", x => { });
            });

        if (testRepository)
        {
            TestNativeInjectorBootStrapper.RegisterServices(serviceCollection);
        }
        else
        {
            NativeInjectorBootStrapper.RegisterServices(serviceCollection);
        }

        ServiceProvider = serviceCollection.AddLogging().BuildServiceProvider();
    }

    public Context getServiceContext()
    {
        return ServiceProvider.GetService<Context>();
    } 
}