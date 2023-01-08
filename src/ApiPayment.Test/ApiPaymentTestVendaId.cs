using System.Text.Json;
using ApiPayment.Application.Services;
using ApiPayment.CC.Dto.ViewModels;
using ApiPayment.Data.Contexts;
using ApiPayment.Domain.Repositories;
using ApiPayment.Test.DataBase;

namespace ApiPayment.Test;

public class ApiPaymentTestListVendaId : IClassFixture<DbFixture>
{
    protected VendaService _vendaService;
    protected Mock<IVendaRepository> _vendaResositoryMock;
    protected IConfiguration _configuration;
    protected ILogger<VendaService> _logger;
    private ServiceProvider _serviceProvider;
    protected Context db;
    

    public ApiPaymentTestListVendaId(DbFixture fixture)
    {
        fixture.Init();
        _serviceProvider = fixture.ServiceProvider;
        db = fixture.getServiceContext();
        db.Database.OpenConnection();
        db.Database.EnsureCreated();

        _configuration = this.InitConfiguration();
        var retorno = ListaVendasId();

        _vendaResositoryMock = new Mock<IVendaRepository>();
        _vendaResositoryMock.Setup(m => m.GetVendaId(It.IsAny<int>())).Returns(retorno);

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
    public async Task<VendaViewModel> ListaVendasId(){

        var listaJson = "{\n  \"numeroPedido\": 5,\n  \"data\": \"2023-01-07T22:58:32.529\",\n  \"vendedor\": {\n    \"id\": 4,\n    \"cpf\": \"000.000.000-00\",\n    \"nome\": \"Nome Vendedor\",\n    \"email\": \"email@live.com\",\n    \"telefone\": \"99 99999 9999\"\n  },\n  \"itens\": [\n    {\n      \"produto\": \"Iphone X\",\n      \"quantidade\": 3,\n      \"valorUnitario\": 3401.12\n    }\n  ],\n  \"status\": \"PagamentoAprovado\"\n}";
        var retorno = JsonSerializer.Deserialize<VendaViewModel>(listaJson);

        return await Task.FromResult(retorno);
    }

    [Theory(DisplayName = "ListarVendasId")]
    [InlineData(5)]
    public void BuscarVendasId(int numeroDoPedido)
    {
        //Arrange
        var listaJson = "{\n  \"numeroPedido\": 5,\n  \"data\": \"2023-01-07T22:58:32.529\",\n  \"vendedor\": {\n    \"id\": 4,\n    \"cpf\": \"000.000.000-00\",\n    \"nome\": \"Nome Vendedor\",\n    \"email\": \"email@live.com\",\n    \"telefone\": \"99 99999 9999\"\n  },\n  \"itens\": [\n    {\n      \"produto\": \"Iphone X\",\n      \"quantidade\": 3,\n      \"valorUnitario\": 3401.12\n    }\n  ],\n  \"status\": \"PagamentoAprovado\"\n}";
        var vendaId = JsonSerializer.Deserialize<VendaViewModel>(listaJson);
        var result = _vendaService.GetVendaId(numeroDoPedido);

        //Assert
        Assert.Equal(vendaId.NumeroPedido, result.Result.NumeroPedido);
        Assert.IsType<VendaViewModel>(vendaId);
    }
}