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
        public DateTime DataNascimento { get; set; }
                
        public int Idade { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Cidade inválida.")]
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
    }
}
