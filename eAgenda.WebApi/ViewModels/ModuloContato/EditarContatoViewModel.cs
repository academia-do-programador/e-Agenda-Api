﻿namespace eAgenda.WebApi.ViewModels.ModuloContato
{
    public class EditarContatoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
    }
}
