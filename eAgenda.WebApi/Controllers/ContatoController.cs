using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.WebApi.Controllers
{
    [ApiController]
    [Route("api/contatos")]    
    public class ContatoController : ControllerBase
    {
        [HttpGet]
        public List<ListarContatoViewModel> SeleciontarTodos(StatusFavoritoEnum statusFavorito)
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

            var servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);

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
                    Email = contato.Email,
                    Telefone = contato.Telefone
                };

                contatosViewModel.Add(contatoViewModel);
            }

            return contatosViewModel;
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarContatoViewModel SeleciontarPorId(string id)
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

            var servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);

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

    #region view models de contato
    public class VisualizarContatoViewModel
    {
        public VisualizarContatoViewModel()
        {
            Compromissos = new List<ListarCompromissoViewModel>();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public List<ListarCompromissoViewModel> Compromissos { get; set; }
    }
    
    public class ListarContatoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }

    #endregion  

    #region view models de compromisso
    public class ListarCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public DateTime Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
    }

    #endregion

}
