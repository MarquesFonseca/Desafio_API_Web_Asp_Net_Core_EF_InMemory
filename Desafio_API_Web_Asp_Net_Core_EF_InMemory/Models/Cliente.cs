using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 2 e 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 100 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(1, ErrorMessage = "Este campo deve ser 'M' (Masculino) ou 'F' (Feminino).'")]
        [MinLength(1, ErrorMessage = "Este campo deve ser 'M' (Masculino) ou 'F' (Feminino).'")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Display(Name = "Infome uma data de Nascimento")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Cidade inválida.")]
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
    }

    public static class FormatarCliente
    {
        /// <summary>
        /// Método usado para formatar os campos de um objeto Cliente
        /// </summary>
        /// <param name="_cliente">Informe aqui o objeto Cliente.</param>
        /// <returns>Retorna o objeto Cliente já formatado.</returns>
        public static Cliente FormatarCampos(this Cliente _cliente)
        {
            _cliente.NomeCompleto = _cliente.NomeCompleto.ToUpperInvariant();
            _cliente.Idade = Utils.FormatacaoData.RetornaIdade(Convert.ToDateTime(_cliente.DataNascimento));
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
