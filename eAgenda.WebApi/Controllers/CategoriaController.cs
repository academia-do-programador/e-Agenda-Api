using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.WebApi.ViewModels.ModuloDespesa;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    //[Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private readonly IMapper mapeadorCategorias;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeadorCategorias)
        {
            this.servicoCategoria = servicoCategoria;
            this.mapeadorCategorias = mapeadorCategorias;
        }

        [HttpGet]
        public void SelecionarTodos()
        {
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public void SelecionarCategoriaCompletoPorId(Guid id)
        {
        }

        [HttpGet("{id}")]
        public void SelecionarCategoriaPorId(Guid id)
        {
        }

        [HttpPost]
        public void Inserir(InserirCategoriaViewModel categoriaVM)
        {
        }

        [HttpPut("{id}")]
        public void Editar(Guid id, EditarCategoriaViewModel categoriaVM)
        {
        }

        [HttpDelete("{id}")]
        public void Excluir(Guid id)
        {
        }
    }
}
