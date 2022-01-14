using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Utils;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models
{
    public class Cidade
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 2 e 100 caracteres.")]
        [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(2, ErrorMessage = "Este campo deve conter 2 caracteres.")]
        [MinLength(2, ErrorMessage = "Este campo deve conter 2 caracteres.")]
        public string EstadoUF { get; set; }
    }
    public static class FormatarCidade
    {
        /// <summary>
        /// Método usado para formatar os campos de uma cidade.
        /// </summary>
        /// <param name="_cidade">Informe aqui o objeto Cidade.</param>
        /// <returns>Retorna o objeto Cidade já formatada.</returns>
        public static Cidade FormatarCampos(this Cidade _cidade)
        {
            if (_cidade == null) return _cidade;
            _cidade.Nome = _cidade.Nome.Trim().IniciaisMaiusculoDeCadaPalavra();
            _cidade.EstadoUF = _cidade.EstadoUF.ToUpper().Trim().IniciaisMaiusculoDeCadaPalavra();
            return _cidade;
        }

        /// <summary>
        /// Método usado para formatar os campos de uma lista de Cidades 
        /// </summary>
        /// <param name="_cidade">Informe uma lista de Cidades</param>
        /// <returns>Retorna a lista informada já formatada</returns>
        public static List<Cidade> FormatarCampos(this List<Cidade> _cidade)
        {
            foreach (Cidade item in _cidade)
                FormatarCampos(item);
            return _cidade;
        }
    }
}
