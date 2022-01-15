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
}
