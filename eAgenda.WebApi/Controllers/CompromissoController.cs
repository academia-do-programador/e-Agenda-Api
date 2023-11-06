using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
using Microsoft.AspNetCore.Authorization;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/compromissos")]
    [ApiController]
    [Authorize]
    public class CompromissoController : ControllerBase
    {
        private readonly ServicoCompromisso servicoCompromisso;
        private readonly IMapper mapeadorCompromissos;

        public CompromissoController(ServicoCompromisso servicoCompromisso, IMapper mapeadorCompromissos)
        {
            this.servicoCompromisso = servicoCompromisso;
            this.mapeadorCompromissos = mapeadorCompromissos;
        }

        [HttpGet]
        public void SelecionarTodos()
        {
        }

        [HttpGet, Route("hoje/{dataAtual:datetime}")]
        public void SelecionarCompromissosDeHoje(DateTime dataAtual)
        {
        }

        [HttpGet, Route("futuros/{dataInicial:datetime}={dataFinal:datetime}")]
        public void SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal)
        {
        }

        [HttpGet, Route("passados/{dataAtual:datetime}")]
        public void SelecionarCompromissosPassados(DateTime dataAtual)
        {
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public void SelecionarCompromissoCompletoPorId(Guid id)
        {
        }

        [HttpGet("{id}")]
        public void SelecionarCompromissoPorId(Guid id)
        {
        }

        [HttpPost]
        public void Inserir(InserirCompromissoViewModel compromissoVM)
        {
        }

        [HttpPut("{id}")]
        public void Editar(Guid id, EditarCompromissoViewModel compromissoVM)
        {
        }

        [HttpDelete("{id}")]
        public void Excluir(Guid id)
        {
        }
    }
}
