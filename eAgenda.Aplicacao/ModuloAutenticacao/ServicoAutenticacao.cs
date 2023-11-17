using eAgenda.Dominio.ModuloAutenticacao;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eAgenda.Aplicacao.ModuloAutenticacao
{
    public class ServicoAutenticacao : ServicoBase<Usuario, ValidadorUsuario>
    {
        private readonly UserManager<Usuario> userManager;

        public ServicoAutenticacao(UserManager<Usuario> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<Usuario>> RegistrarAsync(Usuario usuario, string senha)
        {
            Result resultado = Validar(usuario);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            IdentityResult usuarioResult = await userManager.CreateAsync(usuario, senha);

            if (usuarioResult.Succeeded == false)
                return Result.Fail(usuarioResult.Errors
                    .Select(erro => new Error(erro.Description)));

            return Result.Ok(usuario);
        }
    }
}
