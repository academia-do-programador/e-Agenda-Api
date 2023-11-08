using eAgenda.Dominio;
using eAgenda.Dominio.ModuloContato;
using FluentResults;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eAgenda.Aplicacao.ModuloContato
{
    public class ServicoContato : ServicoBase<Contato, ValidadorContato>
    {
        private IRepositorioContato repositorioContato;
        private IContextoPersistencia contextoPersistencia;

        public ServicoContato(IRepositorioContato repositorioContato,
                             IContextoPersistencia contexto)
        {
            this.repositorioContato = repositorioContato;
            this.contextoPersistencia = contexto;
        }

        public async Task<Result<Contato>> Inserir(Contato contato)
        {            
            Result resultado = Validar(contato);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioContato.Inserir(contato);

                await contextoPersistencia.GravarDadosAsync();
                               
                return Result.Ok(contato);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir o Contato";
                
                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<Contato>> Editar(Contato contato)
        {
            Log.Logger.Debug("Tentando editar contato... {@c}", contato);

            var resultado = Validar(contato);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioContato.Editar(contato);

                await contextoPersistencia.GravarDadosAsync();

                Log.Logger.Information("Contato {ContatoId} editado com sucesso", contato.Id);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar editar o Contato";

                Log.Logger.Error(ex, msgErro + " {ContatoId}", contato.Id);

                throw new Exception(msgErro, ex);
            }

            return Result.Ok(contato);
        }

        public async Task<Result> Excluir(Guid id)
        {
            try
            {
                var contatoResult = SelecionarPorId(id);

                if (contatoResult.IsSuccess)
                    return await Excluir(contatoResult.Value);

                return Result.Fail(contatoResult.Errors);
            }
            catch(Exception exc)
            {
                string msgErro = "Falha no sistema ao tentar editar o Contato";

                Log.Logger.Error(exc, msgErro + " {ContatoId}", id);

                throw new Exception(msgErro, exc);
            }
        }
        
        public async Task<Result> Excluir(Contato contato)
        {
            Log.Logger.Debug("Tentando excluir contato... {@c}", contato);

            try
            {
                repositorioContato.Excluir(contato);

                await contextoPersistencia.GravarDadosAsync();

                Log.Logger.Information("Contato {ContatoId} editado com sucesso", contato.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar excluir o Contato";

                Log.Logger.Error(ex, msgErro + " {ContatoId}", contato.Id);

                throw new Exception(msgErro, ex);
            }
        }

        //Task<Result<List<Contato>>>
        //  -> Retorna vários contatos de maneira fácil a manipulação
        //  -> Numa estrutura que facilita a resposta de sucesso ou falha 
        //  -> De maneira assíncrona

        //Contato[] -> Retorna vários contatos
        public async Task<Result<List<Contato>>> SelecionarTodos(StatusFavoritoEnum statusFavorito)
        {
            Log.Logger.Debug("Tentando selecionar contatos...");

            try
            {
                var contatos = await repositorioContato.SelecionarTodosAsync(statusFavorito);

                Log.Logger.Information("Contatos selecionados com sucesso");

                return Result.Ok(contatos);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os Contatos";

                Log.Logger.Error(ex, msgErro);

                throw new Exception(msgErro, ex);
            }
        }

        public Result<Contato> SelecionarPorId(Guid id)
        {
            Log.Logger.Debug("Tentando selecionar contato {ContatoId}...", id);

            try
            {
                var contato = repositorioContato.SelecionarPorId(id);

                if (contato == null)
                {
                    Log.Logger.Warning("Contato {ContatoId} não encontrado", id);

                    return Result.Fail($"Contato {id} não encontrado");
                }

                Log.Logger.Information("Contato {ContatoId} selecionado com sucesso", id);

                return Result.Ok(contato);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Contato";

                Log.Logger.Error(ex, msgErro + " {ContatoId}", id);

                throw new Exception(msgErro, ex);
            }
        }

        public Result<Contato> ConfigurarFavoritos(Contato contato)
        {
            Log.Logger.Debug("Tentando favoritar contato {ContatoId}...", contato.Id);

            try
            {
                contato.ConfigurarFavorito();

                repositorioContato.Editar(contato);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Contato {ContatoId} favoritado com sucesso", contato.Id);

                return Result.Ok(contato);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar favoritar o Contato";

                Log.Logger.Error(ex, msgErro + " {ContatoId}", contato.Id);

                throw new Exception(msgErro, ex);
            }
        }
    }
}
