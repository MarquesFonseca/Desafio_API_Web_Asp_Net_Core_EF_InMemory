using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Controllers
{
    [ApiController]
    [Route("cidade")]
    public class CidadeController : Controller
    {
        /// <summary>
        /// Retorna uma lista de cidade, usando o Task de forma Assíncrona.
        /// </summary>
        /// <param name="dataContext">Acessa os dados. O "[FromServices"] indica que vai utilizar o DataContect que já está na memória</param>
        /// <returns>Retorna toda lista de Cidades</returns>
        [HttpGet]//definindo o verbo utilizado. Se não colocar nada, por padrão ele já usa o GET
        [Route("")]//Rota vazia, ou seja, será a mesa rota definida no controller.
        public async Task<ActionResult<List<Cidade>>> Get([FromServices] DataContext dataContext)
        {
            var cidades = await dataContext.Cidades.AsNoTracking().ToListAsync();
            return cidades;
        }

        


    }
}
