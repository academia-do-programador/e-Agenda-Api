using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Http.Extensions;

namespace eAgenda.WebApi.Controllers
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatoController : ControllerBase
    {
        private ServicoContato servicoContato;
        private IMapper mapeador;

        public ContatoController(ServicoContato servicoContato, IMapper mapeador)
        {
            this.mapeador = mapeador;
            this.servicoContato = servicoContato;
        }

        [HttpGet]
        public List<ListarContatoViewModel> SeleciontarTodos(StatusFavoritoEnum statusFavorito)
        {
            var contatos = servicoContato.SelecionarTodos(statusFavorito).Value;

            return mapeador.Map<List<ListarContatoViewModel>>(contatos);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarContatoViewModel SeleciontarPorId(Guid id)
        {
            var contato = servicoContato.SelecionarPorId(id).Value;

            return mapeador.Map<VisualizarContatoViewModel>(contato);
        }

        [HttpPost]
        public IActionResult Inserir(InserirContatoViewModel contatoViewModel)
        {
            try
            {
                var contato = mapeador.Map<Contato>(contatoViewModel);

                var resultado = servicoContato.Inserir(contato);

                if (resultado.IsFailed)
                    return BadRequest(resultado.Errors.Select(x => x.Message));

                var enderecoContato = Request.GetDisplayUrl() + "/visualizacao-completa/" + resultado.Value.Id;

                return Created(enderecoContato, contatoViewModel);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Editar(Guid id, EditarContatoViewModel contatoViewModel)
        {
            try
            {
                var resultadoSelecao = servicoContato.SelecionarPorId(id);

                if (resultadoSelecao.IsFailed)
                    return NotFound(resultadoSelecao.Errors.Select(x => x.Message));

                var contato = mapeador.Map(contatoViewModel, resultadoSelecao.Value);

                var resultadoInsercao = servicoContato.Editar(contato);

                if (resultadoInsercao.IsFailed)
                    return BadRequest(resultadoInsercao.Errors.Select(x => x.Message));

                var enderecoContato = Request.GetDisplayUrl() + "/visualizacao-completa/" + resultadoInsercao.Value.Id;

                return Ok(contatoViewModel);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(Guid id)
        {
            try
            {
                var resultadoSelecao = servicoContato.SelecionarPorId(id);

                if (resultadoSelecao.IsFailed)
                    return NotFound(resultadoSelecao.Errors.Select(x => x.Message));

                var contato = resultadoSelecao.Value;

                var resultadoExclusao = servicoContato.Excluir(contato);

                if (resultadoExclusao.IsFailed)
                    return BadRequest(resultadoExclusao.Errors.Select(x => x.Message));

                return Ok();
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }

    }
}
