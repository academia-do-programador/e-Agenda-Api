using FluentResults;

namespace eAgenda.WebApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ProcessarResultado(Result result, object viewModel = null)
        {
            if (result.IsFailed)
                return BadRequest(new
                {
                    Sucesso = false,
                    Erros = result.Errors.Select(x => x.Message)
                });

            return Ok(new
            {
                Sucesso = true,
                Dados = viewModel
            });
        }

        public override OkObjectResult Ok(object? dados)
        {
            return Ok(new
            {
                Sucesso = true,
                Dados = dados
            });
        }

        public override NotFoundObjectResult NotFound(object erros)
        {
            List<IError> mensagensDeErro = erros as List<IError>;

            if (mensagensDeErro == null)
                throw new InvalidCastException(nameof(erros) + " não é uma lista de IError");

            return NotFound(new
            {
                Sucesso = false,
                Erros = mensagensDeErro.Select(x => x.Message)
            });
        }

        public override BadRequestObjectResult BadRequest(object erros)
        {
            List<IError> mensagensDeErro = erros as List<IError>;

            if (mensagensDeErro == null)
                throw new InvalidCastException(nameof(erros) + " não é uma lista de IError");

            return BadRequest(new
            {
                Sucesso = false,
                Erros = mensagensDeErro.Select(x => x.Message)
            });
        }
    }
}