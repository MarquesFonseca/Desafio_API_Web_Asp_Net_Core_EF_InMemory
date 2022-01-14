using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Utils;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> GetClientes([FromServices] DataContext context)
        {
            var clientes = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .ToListAsync();

            return clientes.FormatarCampos();
        }        

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cliente>> GetClienteById([FromServices] DataContext context, int id)
        {
            var cliente = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return cliente.FormatarCampos();
        }

        [HttpGet]
        [Route("pesquisar/nome/{nomeCliente}")]
        public async Task<ActionResult<List<Cliente>>> GetClienteByParteNomeCompleto([FromServices] DataContext context, string nomeCliente)
        {

            var cliente = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .Where(x => x.NomeCompleto.ToUpper().RemoveAcentos().Contains(nomeCliente.ToUpper().RemoveAcentos()))
                .ToListAsync();

            return cliente.FormatarCampos();
        }

        /// <summary>
        /// Retorna todos os clientes de uma cidade específica
        /// </summary>
        /// <param name="context"></param>
        /// <param name="CidadeId"></param>
        /// <returns>Retorna uma lista de clientes de uma mesma cidade.</returns>
        [HttpGet]
        [Route("pesquisar/cidade/{CidadeId:int}")]
        public async Task<ActionResult<List<Cliente>>> GetClientesByCidadeId([FromServices] DataContext context, int CidadeId)
        {
            var clientes = await context.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .Where(x => x.CidadeId == CidadeId)
                .ToListAsync();

            return clientes.FormatarCampos();
        }

        /// <summary>
        /// Insere um novo Cliente
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns>Retorna o Cliente recém inserido.</returns>
        [HttpPost]
        [Route("novo")]
        public async Task<ActionResult<Cliente>> CreateCidade([FromServices] DataContext context, [FromBody] Cliente model)
        {
            if (Utils.FormatacaoData.VerificaDataSeValida(model.DataNascimento) == false)
                return NotFound("Não foi possível gravar os dados do cliente. \nCidade Inválida.\nInform no fomrato 'ano-mes-dia'");

            bool existeACidadeInformada = await context.Cidades.AnyAsync(x => x.Id == model.CidadeId);
            if (existeACidadeInformada == false)//vai inserir um novo cliente se existir a cidade informada.
                return NotFound("Não foi possível gravar os dados do cliente. Cidade Inválida.");

            try
            {
                if (ModelState.IsValid)
                {
                    model.FormatarCampos();

                    model.Cidade = await context.Cidades.FindAsync(model.CidadeId);                    
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
            catch (Exception)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao gravar o registro.!");
            }
        }
        /// <summary>
        /// Retorna todos os clientes cadastrados
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Produtos</returns>

        [HttpPut("{id}")]
        [Route("alterar/{id:int}")]
        public async Task<ActionResult<Cliente>> AlterarCliente([FromServices] DataContext dataContext, [FromBody] Cliente model, int id)
        {
            #region verifica se a data é válida
            if (Utils.FormatacaoData.VerificaDataSeValida(model.DataNascimento) == false)
                return NotFound("Não foi possível gravar os dados do cliente. \nCidade Inválida.\nInform no fomrato 'ano-mes-dia'");
            #endregion

            #region verifica se a cidade informada existe
            bool existeACidadeInformada = await dataContext.Cidades.AnyAsync(x => x.Id == model.CidadeId);
            if (existeACidadeInformada == false)//vai inserir um novo cliente se existir a cidade informada.
                return NotFound("Não foi possível gravar os dados do cliente. Cidade Inválida.");
            #endregion

            #region Verifica se o cliente existe no banco
            bool existe = await dataContext.Clientes.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound("Cliente não encontrado");
            #endregion

            if (model.Id != id)
            {
                return BadRequest();
            }

            var cliente = new Cliente();

            try
            {
                model.FormatarCampos();

                model.Cidade = await dataContext.Cidades.FindAsync(model.CidadeId);
                model.DataCadastro = DateTime.Now;

                dataContext.Entry(model).State = EntityState.Modified;
                await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                cliente = await dataContext.Clientes.FindAsync(id);
                if (cliente == null)
                    //return BadRequest();
                    return NotFound("Ocorreu um erro ao alterar o cliente atual. Não foi possível retornar o cliente.");
                else
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao remover o registro.!");
            }

            var retorno = await GetClienteById(dataContext, id);
            return retorno;
        }

        /// <summary>
        /// Deleta/Remove um cliente repassando via "Rota" o id desejado para exclusão
        /// </summary>
        /// <param name="context">Representação do nosso banco de dados</param>
        /// <param name="id">Informe o Id desejado para exclusão</param>
        /// <returns>Não retorna nada.</returns>
        [HttpDelete("{id}")]
        [Route("remover/{id:int}")]
        public async Task<IActionResult> RemoverCliente([FromServices] DataContext context, int id)
        {
            var clienteAtual = await context.Clientes.FindAsync(id);
            if (clienteAtual == null)
            {
                //return BadRequest();
                return NotFound("O Cliente não foi localizada!");
            }

            try
            {
                context.Clientes.Remove(clienteAtual);
                await context.SaveChangesAsync();

                //return NotFound("Cliente removida com sucesso!");
                return Ok("Cliente removida com sucesso!");
            }
            catch (Exception)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao remover o registro.!");
            }
        }
    }
}
