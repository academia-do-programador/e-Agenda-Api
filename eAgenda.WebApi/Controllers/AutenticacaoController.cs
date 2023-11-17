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

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarUsuarioViewModel viewModel)
        {
            var usuario = mapeador.Map<Usuario>(viewModel);
            
            var usuarioResult = await servicoAutenticacao.RegistrarAsync(usuario, viewModel.Senha);

            return ProcessarResultado(usuarioResult.ToResult(), viewModel);            
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar(AutenticarUsuarioViewModel viewModel)
        {
            var usuarioResult = await servicoAutenticacao.Autenticar(viewModel.Login, viewModel.Senha);

            return ProcessarResultado(usuarioResult.ToResult(), viewModel);
        }

        [HttpPost("sair")]
        public async Task<IActionResult> Sair()
        {
            await servicoAutenticacao.Sair();

            return Ok();
        }
    }
}
