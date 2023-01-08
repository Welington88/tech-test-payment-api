using System.Text.Json;
using ApiPayment.Application.Services;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Data.Contexts;
using ApiPayment.Domain.Repositories;
using ApiPayment.Test.DataBase;

namespace ApiPayment.Test;

public class ApiPaymentTestListVenda : IClassFixture<DbFixture>
{
    protected VendaService _vendaService;
    protected Mock<IVendaRepository> _vendaResositoryMock;
    protected IConfiguration _configuration;
    protected ILogger<VendaService> _logger;
    private ServiceProvider _serviceProvider;
    protected Context db;
    

    public ApiPaymentTestListVenda(DbFixture fixture)
    {
        fixture.Init();
        _serviceProvider = fixture.ServiceProvider;
        db = fixture.getServiceContext();
        db.Database.OpenConnection();
        db.Database.EnsureCreated();

        _configuration = this.InitConfiguration();
        var retorno = ListaVendas();

        _vendaResositoryMock = new Mock<IVendaRepository>();
        _vendaResositoryMock.Setup(m => m.Get()).Returns(retorno);

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

        var listaJson = "[\n  {\n    \"numeroPedido\": 5,\n    \"data\": \"2023-01-07T22:58:32.529\",\n    \"vendedor\": {\n      \"id\": 4,\n      \"cpf\": \"000.000.000-00\",\n      \"nome\": \"Nome Vendedor\",\n      \"email\": \"email@live.com\",\n      \"telefone\": \"99 99999 9999\"\n    },\n    \"itens\": [\n      {\n        \"produto\": \"Iphone X\",\n        \"quantidade\": 3,\n        \"valorUnitario\": 3401.12\n      }\n    ],\n    \"status\": \"PagamentoAprovado\"\n  },\n  {\n    \"numeroPedido\": 6,\n    \"data\": \"2023-01-08T02:39:48.621\",\n    \"vendedor\": {\n      \"id\": 5,\n      \"cpf\": \"000000000\",\n      \"nome\": \"Nome Vendedor\",\n      \"email\": \"email@live.com\",\n      \"telefone\": \"99 99999 9999\"\n    },\n    \"itens\": [\n      {\n        \"produto\": \"Notebook Asus\",\n        \"quantidade\": 1,\n        \"valorUnitario\": 2756.57\n      }\n    ],\n    \"status\": \"AguardandoPagamento\"\n  }\n]";
        var retorno = JsonSerializer.Deserialize<List<VendaViewModel>>(listaJson);

        return await Task.FromResult(retorno);
    }

    [Fact(DisplayName = "ListarVendas")]
    public void BuscarVendas()
    {
        //Arrange
        var listaJson = "[\n  {\n    \"numeroPedido\": 5,\n    \"data\": \"2023-01-07T22:58:32.529\",\n    \"vendedor\": {\n      \"id\": 4,\n      \"cpf\": \"000.000.000-00\",\n      \"nome\": \"Nome Vendedor\",\n      \"email\": \"email@live.com\",\n      \"telefone\": \"99 99999 9999\"\n    },\n    \"itens\": [\n      {\n        \"produto\": \"Iphone X\",\n        \"quantidade\": 3,\n        \"valorUnitario\": 3401.12\n      }\n    ],\n    \"status\": \"PagamentoAprovado\"\n  },\n  {\n    \"numeroPedido\": 6,\n    \"data\": \"2023-01-08T02:39:48.621\",\n    \"vendedor\": {\n      \"id\": 5,\n      \"cpf\": \"000000000\",\n      \"nome\": \"Nome Vendedor\",\n      \"email\": \"email@live.com\",\n      \"telefone\": \"99 99999 9999\"\n    },\n    \"itens\": [\n      {\n        \"produto\": \"Notebook Asus\",\n        \"quantidade\": 1,\n        \"valorUnitario\": 2756.57\n      }\n    ],\n    \"status\": \"AguardandoPagamento\"\n  }\n]";
        var listaVendas = JsonSerializer.Deserialize<List<VendaViewModel>>(listaJson);
        var listaVendasAssicrona = _vendaService.Get();
        var result = listaVendasAssicrona.Result.Count();
        //Assert
        Assert.Equal(listaVendas.Count, result);
        Assert.IsType<List<VendaViewModel>>(listaVendasAssicrona.Result);
    }
}