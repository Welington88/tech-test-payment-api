using ApiPayment.Application.Interfaces;
using ApiPayment.CC.Dto.DTOs;
using ApiPayment.CC.Dto.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
namespace ApiPayment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _vendaService;
    private readonly ILogger<VendaController> _logger;
    private readonly IConfiguration _configuration;

    public VendaController(IVendaService vendaService, ILogger<VendaController> logger, IConfiguration configuration)
    {
        _vendaService = vendaService;
        _configuration = configuration;
        _logger = logger;
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de todas vendas.",
        Description = "Retorna lista de todas as vendas.")]
    [SwaggerResponse(200, @"ExisteVendas")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista")]
    public async Task<ActionResult> ListaTodasAsVendas()
    {
        try
        {
            var viewModel = await _vendaService.Get();

            _logger.LogInformation(1, "[API] [Venda] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Venda] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna lista de Vendas por Número do Pedido.",
        Description = "Retorna lista de Vendas por Número do Pedido.")]
    [SwaggerResponse(200, @"ExisteVendas")]
    [SwaggerResponse(400, @"Erro ao retornar dados.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Lista/{numeroPedido}")]
    public async Task<ActionResult> ListaVendasId(int numeroPedido)
    {
        try
        {
            var viewModel = await _vendaService.GetVendaId(numeroPedido);

            _logger.LogInformation(1, "[API] [Venda] [GET] [SUCESSO].");
            return Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(2 ,"[API] [Venda] [GET] [Existe] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Salva envio de Venda.",
        Description = "Salva os dados Venda.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Venda.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("CriarPedido")]
    public async Task<ActionResult> CriarVenda(VendaDTO vendaDto)
    {
        try
        {
            var result = await _vendaService.PostVenda(vendaDto);
            if (!result)
            {
                return BadRequest("Erro ao Criar Venda ");
            }
            _logger.LogInformation(1, "[API] [Venda] [Post] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Venda] [Post] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualiza envio de Venda.",
        Description = "Atualiza os dados Venda.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Venda.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Atualizar/{numeroPedido}")]
    public async Task<ActionResult> AtualizarVenda(int numeroPedido, VendaDTO vendaDto)
    {
        try
        {
            var vendaBanco = await _vendaService.GetVendaId(numeroPedido);

            var result = await _vendaService.PutVenda(numeroPedido,vendaDto,vendaBanco.Status);
            if (!result)
            {
                return BadRequest("Erro ao Atualizar Venda ");
            }
            _logger.LogInformation(1, "[API] [Venda] [Put] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Venda] [Put] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Atualiza Status de Venda.",
        Description = "Atualiza os dados Venda.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Venda.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("AtualizarStatus/{numeroPedido}/{status}")]
    public async Task<ActionResult> AtualizarStatusVenda(int numeroPedido, Status status)
    {
        try
        {
            var vendaBanco = await _vendaService.GetVendaId(numeroPedido);

            var vendedorDto = new VendedorDTO() {
                Cpf = vendaBanco.Vendedor.Cpf,
                Email = vendaBanco.Vendedor.Email,
                Id = vendaBanco.Vendedor.Id,
                Nome = vendaBanco.Vendedor.Nome,
                Telefone = vendaBanco.Vendedor.Telefone
            };

            var itensDto = new List<VendaDTO.ItensVenda>();

            foreach (var item in vendaBanco.Itens)
            {
                var itemDto = new VendaDTO.ItensVenda() {
                    Produto = item.Produto,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario

                };
                itensDto.Add(itemDto);
            }

            var vendaDto = new VendaDTO() {
                Data = vendaBanco.Data,
                Vendedor = vendedorDto,
                NumeroPedido = vendaBanco.NumeroPedido,
                Itens = itensDto,
                Status = status
            };

            var result = await _vendaService.PutVenda(numeroPedido, vendaDto,vendaBanco.Status);
            if (!result)
            {
                return BadRequest("Erro ao Atualizar Status Venda ");
            }
            _logger.LogInformation(1, "[API] [Venda] [Put] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Venda] [Put] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Deleta envio de Venda.",
        Description = "Deleta envio de Venda.")]
    [SwaggerResponse(200, @"bool")]
    [SwaggerResponse(400, @"Erro ao salvar dados de um Venda.")]
    [SwaggerResponse(500, @"Erro")]
    [Route("Excluir/{numeroDoPedido}")]
    public async Task<ActionResult> ExcluirVenda(int numeroDoPedido)
    {
        try
        {
            var result = await _vendaService.DeleteVenda(numeroDoPedido);
            if (!result)
            {
                return BadRequest("Erro ao Excluir Venda ");
            }
            _logger.LogInformation(1, "[API] [Venda] [Delete] [SUCESSO] - Documento salvo com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(2, "[API] [Venda] [Delete] [FALHA] - " + ex.Message);

            return BadRequest(ex);
        }
    }
}