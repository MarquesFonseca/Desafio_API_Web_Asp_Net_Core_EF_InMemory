using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using System;
using System.Collections.Generic;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Helpers
{
    public static class FormatarCliente
    {
        /// <summary>
        /// Método usado para formatar os campos de um objeto Cliente
        /// </summary>
        /// <param name="_cliente">Informe aqui o objeto Cliente.</param>
        /// <returns>Retorna o objeto Cliente já formatado.</returns>
        public static Cliente FormatarCampos(this Cliente _cliente)
        {
            if (_cliente == null) return _cliente;
            _cliente.NomeCompleto = _cliente.NomeCompleto.Trim().IniciaisMaiusculoDeCadaPalavra();
            _cliente.Idade = FormatacaoData.RetornaIdade(Convert.ToDateTime(_cliente.DataNascimento));
            _cliente.DataNascimento = Convert.ToDateTime(_cliente.DataNascimento.ToShortDateString());
            return _cliente;
        }

        /// <summary>
        /// Método usado para formatar os campos de uma lista de Clientes.
        /// </summary>
        /// <param name="_clientes">Informe aqui a Lista de Clientes.</param>
        /// <returns>Retorna a Lista de Clientes já formatada.</returns>
        public static List<Cliente> FormatarCampos(this List<Cliente> _clientes)
        {
            foreach (Cliente item in _clientes)
                FormatarCampos(item);
            return _clientes;
        }
    }
}
