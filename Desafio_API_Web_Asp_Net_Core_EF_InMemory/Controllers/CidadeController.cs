﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Controllers
{
    [ApiController] //definindo para a classe Contoller, que usaremos o ApiController
    [Route("cidade")] //Como não definimos nenhuma rota no endPoints(Startup.cs), o mapeamento das rotas serão pela rotas do controller atual
    public class CidadeyController : ControllerBase
    {
        /// <summary>
        /// Retorna uma lista de Cidades, usando o Task de forma assíncrona. 
        /// </summary>
        /// <param name="dataContext">Acessar os dados. o "[FromServices]" indica que vai utilizar o DataContext que já está em memória</param>
        /// <returns>Retorna uma lista de Cidades</returns>
        [HttpGet]//definindo o verbo utilizado. Se não colocar nada, por padrão ele assume o GET
        [Route("")]//rota vazia, ou seja, será a mesma rota definida no controller
        public async Task<ActionResult<List<Cidade>>> Get([FromServices] DataContext dataContext)
        {
            var cidades = await dataContext.Cidades.AsNoTracking().ToListAsync();
            return cidades;
        }

        /// <summary>
        /// Retorna uma Lista de Cidades em ordem Decrescente.
        /// É um método privado, usado internamente.
        /// </summary>
        /// <param name="dataContext">Acessar os dados. o "[FromServices]" indica que vai utilizar o DataContext que já está em memória</param>
        /// <returns>Retorna uma lista de Cidades em ordem Descrescente</returns>
        [HttpGet]
        [Route("")]
        private async Task<ActionResult<List<Cidade>>> GetOrderByDesc([FromServices] DataContext dataContext)
        {
            var cidades = await dataContext.Cidades.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return cidades;
        }

        /// <summary>
        /// Retorna uma Cidade informando o Id da Cidade desejada
        /// </summary>
        /// <param name="context">Acessar os dados. o "[FromServices]" indica que vai utilizar o DataContext que já está em memória</param>
        /// <param name="id">Informe o Id da Cidade desejado</param>
        /// <returns>Retorna uma Cidade</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cidade>> GetById([FromServices] DataContext context, int id)
        {
            var cidade = await context.Cidades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return cidade;
        }

        /// <summary>
        /// Verifica se o Id da Cidade existe na base de dados.
        /// Método privado, só acessa internamente.
        /// </summary>
        /// <param name="context">Acessar os dados. o "[FromServices]" indica que vai utilizar o DataContext que já está em memória</param>
        /// <param name="id">Informe o Id da Cidade desejado.</param>
        /// <returns>Retorna "Verdadeiro / Falso"</returns>
        [HttpGet]
        [Route("{id:int}")]
        private async Task<ActionResult<bool>> VerificaSeExiste([FromServices] DataContext context, int id)
        {
            bool existe = await context.Cidades.AnyAsync(x => x.Id == id);
            return existe;
        }

        /// <summary>
        /// Grava na base uma nova Cidade.
        /// E retorna toda a lista das Cidades já cadastradas.
        /// </summary>
        /// <param name="context">Representação do banco de dados em memória</param>
        /// <param name="model">Modelo via json passado pelo Body</param>
        /// <returns>Retorna uma lista de todos.</returns>        
        [HttpPost]
        [Route("novo")]
        public async Task<ActionResult<List<Cidade>>> Post([FromServices] DataContext context, [FromBody] Cidade model)
        {
            if (ModelState.IsValid)
            {
                //System.Guid g = System.Guid.NewGuid();
                //model.Id = g.ToString();
                context.Cidades.Add(model);
                await context.SaveChangesAsync();
                return await GetOrderByDesc(context);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Altera os dados da Cidade atual
        /// </summary>
        /// <param name="context">Representação do banco de dados</param>
        /// <param name="model">Dados a ser alterado no modelo json pelo body</param>
        /// <param name="id">Informe o Id da Cidade desejada</param>
        /// <returns>Retorna a Cidade já alterada.</returns>
        [HttpPut("{id}")]
        [Route("alterar/{id:int}")]
        public async Task<ActionResult<Cidade>> Alterar([FromServices] DataContext context, [FromBody] Cidade model, int id)
        {
            if (model.Id != id)
            {
                return BadRequest();
            }

            bool existe = await context.Cidades.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var cidade = await context.Cidades.FindAsync(id);
                if (cidade == null)
                {
                    //return BadRequest();
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var retorno = await context.Cidades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return retorno;
        }

        /// <summary>
        /// Deleta/Remove a Cidade repassando via "Rota" o id desejado para exclusão
        /// </summary>
        /// <param name="context">Representação do nosso banco de dados</param>
        /// <param name="id">Informe o Id desejado para exclusão</param>
        /// <returns>Não retorna nada.</returns>
        [HttpDelete("{id}")]
        [Route("remover/{id:int}")]
        public async Task<IActionResult> Deletar([FromServices] DataContext context, int id)
        {
            var cidadeAtual = await context.Cidades.FindAsync(id);
            if (cidadeAtual == null)
            {
                //return BadRequest();
                return NotFound();
            }

            context.Cidades.Remove(cidadeAtual);
            await context.SaveChangesAsync();

            return NoContent();
            //return Ok();
        }
    }
}
