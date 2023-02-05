using System.Text.Json;
using ApiPayment.Application.Services;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Data.Contexts;
using ApiPayment.Domain.Repositories;
using ApiPayment.Test.DataBase;
using AutoFixture;

namespace ApiPayment.Test;

public class ApiPaymentTestListVenda : IClassFixture<DbFixture>
{
    protected VendaService _vendaService;
    protected Mock<IVendaRepository> _vendaResositoryMock;
    protected IConfiguration _configuration;
    protected ILogger<VendaService> _logger;
    private ServiceProvider _serviceProvider;
    protected Context db;
    protected List<VendaViewModel> listaVendasFixture = new List<VendaViewModel>();

    public ApiPaymentTestListVenda(DbFixture fixture)
    {
        fixture.Init();
        _serviceProvider = fixture.ServiceProvider;
        db = fixture.getServiceContext();
        db.Database.OpenConnection();
        db.Database.EnsureCreated();

        _configuration = this.InitConfiguration();
        listaVendasFixture = ListaVendas().Result;

        _vendaResositoryMock = new Mock<IVendaRepository>();
        _vendaResositoryMock.Setup(m => m.Get()).Returns(Task.FromResult(listaVendasFixture));

        _vendaService = new VendaService(_configuration, _vendaResositoryMock.Object, _logger);
    }

    private IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        return config.Build();
    }

    //Act
    public async Task<List<VendaViewModel>> ListaVendas(){

        var autoFixture = new Fixture();
        autoFixture.RepeatCount = 2;
        var venda = autoFixture.Create<VendaViewModel>();
        var listaVendas = autoFixture.CreateMany<VendaViewModel>();

        return await Task.FromResult(listaVendas.ToList());
    }

    [Fact(DisplayName = "ListarVendas")]
    public void BuscarVendas()
    {
        //Arrange
        var listaVendasAssicrona = _vendaService.Get();
        var result = listaVendasAssicrona.Result.Count();
        //Assert
        Assert.Equal(listaVendasFixture.Count, result);
        Assert.IsType<List<VendaViewModel>>(listaVendasAssicrona.Result);
    }
}