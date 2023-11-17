using eAgenda.Aplicacao.ModuloAutenticacao;
using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.WebApi.ViewModels.ModuloAutenteticacao;

namespace eAgenda.WebApi.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class AutenticacaoController : ApiControllerBase
    {
        private readonly ServicoAutenticacao servicoAutenticacao;
        private readonly IMapper mapeador;

        public AutenticacaoController(ServicoAutenticacao servicoAutenticacao, IMapper mapeador)
        {
            this.servicoAutenticacao = servicoAutenticacao;
            this.mapeador = mapeador;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarUsuarioViewModel viewModel)
        {
            var usuario = mapeador.Map<Usuario>(viewModel);
            
            var usuarioResult = await servicoAutenticacao.RegistrarAsync(usuario, viewModel.Senha);

            return ProcessarResultado(usuarioResult.ToResult(), viewModel);            
        }
    }
}
