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
        public async Task<ActionResult<List<Cliente>>> GetClientes([FromServices] DataContext context)
        {
            //model.Idade = model.Idade == 0 ? Utils.FormataData.RetornaIdade(model.DataNascimento) : model.Idade; //condição ternária

            var clientes = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .ToListAsync();

            foreach (Cliente item in clientes)
            {
                item.Idade = item.Idade == 0 ? Utils.FormatacaoData.RetornaIdade(Convert.ToDateTime(item.DataNascimento)) : item.Idade; //condição ternária
                item.DataNascimento = Convert.ToDateTime(item.DataNascimento.ToShortDateString());
            }

            return clientes;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cliente>> GetClienteById([FromServices] DataContext context, int id)
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
        /// <param name="CidadeId"></param>
        /// <returns>Retorna uma lista de clientes de uma mesma cidade.</returns>
        [HttpGet]
        [Route("cidades/{id:int}")]
        public async Task<ActionResult<List<Cliente>>> GetClientesByCidadeId([FromServices] DataContext context, int CidadeId)
        {
            var clientes = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .Where(x => x.CidadeId == CidadeId)
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
        [Route("novo")]
        public async Task<ActionResult<Cliente>> CreateCidade(
        [FromServices] DataContext context,
        [FromBody] Cliente model)
        {
            if (Utils.FormatacaoData.VerificaDataSeValida(model.DataNascimento) == false)
                return NotFound("Não foi possível gravar os dados do cliente. \nCidade Inválida.\nInform no fomrato 'ano-mes-dia'");

            bool existeACidadeInformada = await context.Cidades.AnyAsync(x => x.Id == model.CidadeId);
            if (existeACidadeInformada == false)//vai inserir um novo cliente se existir a cidade informada.
                return NotFound("Não foi possível gravar os dados do cliente. Cidade Inválida.");

            if (ModelState.IsValid)
            {
                model.Cidade = await context.Cidades.FindAsync(model.CidadeId);
                model.Idade = model.Idade == 0 ? Utils.FormatacaoData.RetornaIdade(model.DataNascimento) : model.Idade; //condição ternária
                model.DataCadastro = DateTime.Now;

                context.Clientes.Add(model);
                await context.SaveChangesAsync();

                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
