using System.Text.Json;
using ApiPayment.Application.Services;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.Enums;
using ApiPayment.Data.Contexts;
using ApiPayment.Domain.Repositories;
using ApiPayment.Test.DataBase;

namespace ApiPayment.Test;

public class ApiPaymentTestPutVenda : IClassFixture<DbFixture>
{
    protected VendaService _vendaService;
    protected Mock<IVendaRepository> _vendaResositoryMock;
    protected IConfiguration _configuration;
    protected ILogger<VendaService> _logger;
    private ServiceProvider _serviceProvider;
    protected Context db;
    

    public ApiPaymentTestPutVenda(DbFixture fixture)
    {
        fixture.Init();
        _serviceProvider = fixture.ServiceProvider;
        db = fixture.getServiceContext();
        db.Database.OpenConnection();
        db.Database.EnsureCreated();

        _configuration = this.InitConfiguration();
        var retorno = alterarVendaRetorno();

        _vendaResositoryMock = new Mock<IVendaRepository>();
        _vendaResositoryMock.Setup(m => m.PutVenda(It.IsAny<int>(),It.IsAny<VendaDTO>(),It.IsAny<Status>())).Returns(retorno);

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
    public async Task<bool> alterarVendaRetorno(){

        return await Task.FromResult(true);
    }

    [Theory(DisplayName = "AlterarVenda")]
    [InlineData(1)]
    public void AlterarVenda(int numeroPedido)
    {
        //Arrange
        var listaJson = "{\n  \"numeroPedido\": 1,\n  \"data\": \"2023-01-07T22:58:32.529\",\n  \"vendedor\": {\n    \"id\": 5,\n    \"cpf\": \"000.000.000-00\",\n    \"nome\": \"Nome Vendedor\",\n    \"email\": \"email@live.com\",\n    \"telefone\": \"99 99999 9999\"\n  },\n  \"itens\": [\n    {\n      \"produto\": \"Iphone X\",\n      \"quantidade\": 3,\n      \"valorUnitario\": 3401.12\n    }\n  ],\n  \"status\": \"PagamentoAprovado\"\n}";
        var vendaDTO = JsonSerializer.Deserialize<VendaDTO>(listaJson);
        var result = _vendaService.PutVenda(numeroPedido,vendaDTO,vendaDTO.Status);

        //Assert
        Assert.True(result.Result);
        Assert.IsType<bool>(result.Result);
    }
}