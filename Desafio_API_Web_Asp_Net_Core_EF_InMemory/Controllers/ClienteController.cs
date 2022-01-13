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
    [Route("cliente")]
    public class ClienteController : Controller
    {
        /// <summary>
        /// Retorna todos os clientes cadastrados
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Produtos</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> Get([FromServices] DataContext context)
        {
            var clientes = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .ToListAsync();
            return clientes;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cliente>> GetById([FromServices] DataContext context, int id)
        {
            var cliente = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return cliente;
        }

        /// <summary>
        /// Retorna todos os clientes de uma cidade específica
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns>Retorna uma lista de clientes de uma mesma cidade.</returns>
        [HttpGet]
        [Route("cidades/{id:int}")]
        public async Task<ActionResult<List<Cliente>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var clientes = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .Where(x => x.CidadeId == id)
                .ToListAsync();
            return clientes;
        }

        /// <summary>
        /// Insere um novo Cliente
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns>Retorna o Cliente recém inserido.</returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Cliente>> Post(
        [FromServices] DataContext context,
        [FromBody] Cliente model)
        {
            bool existeACidadeInformada = await context.Cidades.AnyAsync(x => x.Id == model.CidadeId);
            if (existeACidadeInformada)//vai inserir um novo cliente se existir a cidade informada.
            {
                if (ModelState.IsValid)
                {
                    context.Clientes.Add(model);
                    await context.SaveChangesAsync();
                    var cidade = await context.Cidades.FindAsync(model.Id);
                    model.Cidade = cidade;
                    return model;
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
