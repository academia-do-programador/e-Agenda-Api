using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;
using Microsoft.AspNetCore.Authorization;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/despesas")]
    [ApiController]
    [Authorize]
    public class DespesaController : ControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;
        private readonly IMapper mapeadorDespesas;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeadorDespesas)
        {
            this.servicoDespesa = servicoDespesa;
            this.mapeadorDespesas = mapeadorDespesas;
        }

        [HttpGet]
        public void SelecionarTodos()
        {
        }

        [HttpGet, Route("ultimos-30-dias")]
        public void SelecionarDespesasUltimos30Dias()
        {
        }

        [HttpGet, Route("antigas")]
        public void SelecionarDespesasAntigas()
        {
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public void SelecionarDespesaCompletoPorId(Guid id)
        {
        }

        [HttpGet("{id}")]
        public void SelecionarDespesaPorId(Guid id)
        {
        }

        [HttpPost]
        public void Inserir(InserirDespesaViewModel despesaVM)
        {
        }

        [HttpPut("{id}")]
        public void Editar(Guid id, EditarDespesaViewModel despesaVM)
        {
        }

        [HttpDelete("{id}")]
        public void Excluir(Guid id)
        {
        }
    }
}
