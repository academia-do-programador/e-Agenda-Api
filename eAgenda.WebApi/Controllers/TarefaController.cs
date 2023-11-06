using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Webapi.ViewModels.ModuloTarefa;
using Microsoft.AspNetCore.Authorization;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/tarefas")]
    [ApiController]
    [Authorize]
    public class TarefaController : ControllerBase
    {
        private readonly ServicoTarefa servicoTarefa;
        private readonly IMapper mapeadorTarefas;

        public TarefaController(ServicoTarefa servicoTarefa, IMapper mapeadorTarefas)
        {
            this.servicoTarefa = servicoTarefa;
            this.mapeadorTarefas = mapeadorTarefas;
        }

        [HttpGet]
        public void SelecionarTodos(StatusTarefaEnum status)
        {
        }


        [HttpGet("visualizacao-completa/{id:guid}")]
        public void SelecionarTarefaCompletaPorId(Guid id)
        {
        }

        [HttpGet("{id}")]
        public void SelecionarTarefaPorId(Guid id)
        {
        }

        [HttpPost]
        public void Inserir(InserirTarefaViewModel tarefaVM)
        {
        }

        [HttpPut("{id}")]
        public void Editar(Guid id, EditarTarefaViewModel tarefaVM)
        {
        }

        [HttpDelete("{id}")]
        public void Excluir(Guid id)
        {
        }
    }
}
