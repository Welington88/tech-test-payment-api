using ApiPayment.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiPayment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _vendedorService;
        private readonly ILogger<VendedorController> _logger;
        private readonly IConfiguration _configuration;

        public VendedorController(IVendedorService vendedorService, ILogger<VendedorController> logger, IConfiguration configuration)
        {
            _vendedorService = vendedorService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna lista de todas vendedores.",
            Description = "Retorna lista de todas as vendedores.")]
        [SwaggerResponse(200, @"ExisteVendas")]
        [SwaggerResponse(400, @"Erro ao retornar dados.")]
        [SwaggerResponse(500, @"Erro")]
        [Route("Lista")]
        public async Task<ActionResult> ListaTodosAsVendedores()
        {
            try
            {
                var viewModel = await _vendedorService.Get();

                _logger.LogInformation(1, "[API] [Venda] [GET] [SUCESSO].");
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(2, "[API] [Venda] [GET] [Existe] [FALHA] - " + ex.Message);

                return BadRequest(ex);
            }
        }


        [HttpGet]
        [SwaggerOperation(
        Summary = "Retorna lista de Vendedores por Número do Pedido.",
        Description = "Retorna lista de Vendores por Número do Pedido.")]
        [SwaggerResponse(200, @"ExisteVendas")]
        [SwaggerResponse(400, @"Erro ao retornar dados.")]
        [SwaggerResponse(500, @"Erro")]
        [Route("Lista/{IdVendedor}")]
        public async Task<ActionResult> ListaVendedorId(int IdVendedor)
        {
            try
            {
                var viewModel = await _vendedorService.GetVendedorId(IdVendedor);

                _logger.LogInformation(1, "[API] [Venda] [GET] [SUCESSO].");
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(2, "[API] [Venda] [GET] [Existe] [FALHA] - " + ex.Message);

                return BadRequest(ex);
            }
        }
    }
}
