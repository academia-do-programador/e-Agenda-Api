using eAgenda.Dominio.ModuloTarefa;

namespace eAgenda.Webapi.ViewModels.ModuloTarefa
{

    public class InserirTarefaViewModel
    {
        public string Titulo { get; set; }

        public PrioridadeTarefaEnum Prioridade { get; set; }

        public List<ItemTarefaViewModel> Itens { get; set; }
    }
}
