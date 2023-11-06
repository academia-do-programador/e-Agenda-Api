﻿using eAgenda.Dominio.ModuloDespesa;

namespace eAgenda.WebApi.ViewModels.ModuloDespesa
{
    public class EditarDespesaViewModel 
    {
        public Guid Id { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<Guid> CategoriasSelecionadas { get; set; }


    }

}