using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;

using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.WebApi.ViewModels.ModuloCompromisso;
using eAgenda.WebApi.ViewModels.ModuloContato;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApi.Controllers
{
    [ApiController]
    [Route("api/contatos")]    
    public class ContatoController : ControllerBase
    {
        private ServicoContato servicoContato;

        public ContatoController()
        {
            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(connectionString);

            var contextoPersistencia = new eAgendaDbContext(builder.Options);

            var repositorioContato = new RepositorioContatoOrm(contextoPersistencia);

            servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);
        }

        [HttpGet]
        public List<ListarContatoViewModel> SeleciontarTodos(StatusFavoritoEnum statusFavorito)
        {            
            var contatos = servicoContato.SelecionarTodos(statusFavorito).Value;

            var contatosViewModel = new List<ListarContatoViewModel>();

            foreach (var contato in contatos)
            {
                var contatoViewModel = new ListarContatoViewModel
                {
                    Id = contato.Id,
                    Nome = contato.Nome,
                    Empresa = contato.Empresa,
                    Cargo = contato.Cargo,                    
                    Telefone = contato.Telefone
                };

                contatosViewModel.Add(contatoViewModel);
            }

            return contatosViewModel;
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarContatoViewModel SeleciontarPorId(string id)
        {           
            var contato = servicoContato.SelecionarPorId(Guid.Parse(id)).Value;

            var contatoViewModel = new VisualizarContatoViewModel
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Empresa = contato.Empresa,
                Cargo = contato.Cargo,
                Email = contato.Email,
                Telefone = contato.Telefone
            };

            foreach (var c in contato.Compromissos)
            {
                var compromissoViewModel = new ListarCompromissoViewModel
                {
                    Id = c.Id,
                    Assunto = c.Assunto,
                    Data = c.Data,
                    HoraInicio = c.HoraInicio.ToString(@"hh\:mm\:ss"),
                    HoraTermino = c.HoraTermino.ToString(@"hh\:mm\:ss")
                };

                contatoViewModel.Compromissos.Add(compromissoViewModel);
            }

            return contatoViewModel;
        }

    }

    

}
