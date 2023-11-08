using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;
using FluentResults;

namespace eAgenda.WebApi.Controllers
{
    //serviços Rest Full

    [ApiController]
    [Route("api/contatos")]
    public class ContatoController : ControllerBase
    {
        private ServicoContato servicoContato;
        private IMapper mapeador;
        private readonly ILogger<ContatoController> logger;

        public ContatoController(ServicoContato servicoContato, IMapper mapeador, ILogger<ContatoController> logger)
        {
            this.mapeador = mapeador;
            this.logger = logger;
            this.servicoContato = servicoContato;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]        
        public async Task<IActionResult> SeleciontarTodos(StatusFavoritoEnum statusFavorito)
        {            
            var contatoResult = await servicoContato.SelecionarTodos(statusFavorito);

            return Ok(new
            {
                Sucesso = true,
                Dados = mapeador.Map<List<ListarContatoViewModel>>(contatoResult.Value),
                QtdRegistros = contatoResult.Value.Count
            });
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SeleciontarPorId(Guid id)
        {
            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = contatoResult.Errors.Select(x => x.Message)
                });

            return Ok(new
            {
                Sucesso = true,
                Dados = mapeador.Map<VisualizarContatoViewModel>(contatoResult)
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirContatoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirContatoViewModel contatoViewModel)
        {
            var contato = mapeador.Map<Contato>(contatoViewModel);

            var contatoResult = await servicoContato.Inserir(contato);

            return ProcessarResultado(contatoResult, contatoViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarContatoViewModel contatoViewModel)
        {
            var resultadoSelecao = servicoContato.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = resultadoSelecao.Errors.Select(x => x.Message)
                });

            var contato = mapeador.Map(contatoViewModel, resultadoSelecao.Value);

            var contatoResult = await servicoContato.Editar(contato);

            return ProcessarResultado(contatoResult, contatoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = servicoContato.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = resultadoSelecao.Errors.Select(x => x.Message)
                });

            var contatoResult = await servicoContato.Excluir(resultadoSelecao.Value);

            return ProcessarResultado(contatoResult);
        }

        private IActionResult ProcessarResultado(Result<Contato> contatoResult, FormsContatoViewModel contatoViewModel = null)
        {
            if (contatoResult.IsFailed)
                return BadRequest(new
                {
                    Sucesso = false,
                    Erros = contatoResult.Errors.Select(x => x.Message)
                });

            return Ok(new
            {
                Sucesso = true,
                Dados = contatoViewModel
            });
        }

    }
}
