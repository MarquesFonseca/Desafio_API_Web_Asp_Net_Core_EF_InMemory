using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Interface;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Repository;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Helpers;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Controllers
{
    [Route("api/[Controller]")] //Como não definimos nenhuma rota no endPoints(Startup.cs), o mapeamento das rotas serão pela rotas do controller atual
    [ApiController] //definindo para a classe Contoller, que usaremos o ApiController
    public class CidadeController : ControllerBase
    {
        private readonly IRepositoryCidade _repositoryCidade;
        public CidadeController(IRepositoryCidade repository)
        {
            _repositoryCidade = repository;
        }

        /// <summary>
        /// Retorna uma lista de Cidades, usando o Task de forma assíncrona. 
        /// </summary>
        /// <param name="dataContext">Acessar os dados. o "[FromServices]" indica que vai utilizar o DataContext que já está em memória</param>
        /// <returns>Retorna uma lista de Cidades</returns>
        [HttpGet]//definindo o verbo utilizado. Se não colocar nada, por padrão ele assume o GET
        [Route("")]//rota vazia, ou seja, será a mesma rota definida no controller
        public async Task<ActionResult> GetCity()
        {
            return Ok(await _repositoryCidade.GetCidades());
        }

        /// <summary>
        /// Retorna uma Cidade informando o Id da Cidade desejada
        /// </summary>
        /// <param name="context">Acessar os dados. o "[FromServices]" indica que vai utilizar o DataContext que já está em memória</param>
        /// <param name="id">Informe o Id da Cidade desejado</param>
        /// <returns>Retorna uma Cidade</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cidade>> GetCidadeById(int id)
        {
            return Ok(await _repositoryCidade.GetCidadeById(id));
        }

        [HttpGet]
        [HttpHead]
        [Route("pesquisar/{nome}")]
        public async Task<ActionResult<List<Cidade>>> GetCidadeByNome(string nome)
        {
            return Ok(await _repositoryCidade.GetCidadeByNomeOrEstado(nome));
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
        public async Task<ActionResult<Cidade>> CadastrarCidade([FromBody] Cidade model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repositoryCidade.CadastrarCidade(model));
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
        [HttpPut]
        [Route("alterar/{id:int}")]
        public async Task<ActionResult<Cidade>> AlterarCidade([FromBody] Cidade model, int id)
        {
            if (model.Id != id)
            {
                return BadRequest();
            }

            bool existe = await _repositoryCidade.SeExisteCidade(id);
            if (!existe)
                return NotFound("Não foi possível gravar os dados do cliente. Cidade Inválida.");

            try
            {
                await _repositoryCidade.AlterarCidade(model);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //return BadRequest();
                return NotFound(ex);
            }

            var retorno = await _repositoryCidade.GetCidadeById(id);
            return retorno;
        }

        /// <summary>
        /// Deleta/Remove a Cidade repassando via "Rota" o id desejado para exclusão
        /// </summary>
        /// <param name="context">Representação do nosso banco de dados</param>
        /// <param name="id">Informe o Id desejado para exclusão</param>
        /// <returns>Não retorna nada.</returns>
        [HttpDelete]
        [Route("remover/{id:int}")]
        public async Task<IActionResult> RemoverCidade(int id)
        {
            var cidadeAtual = await _repositoryCidade.GetCidadeById(id);
            if (cidadeAtual == null)
            {
                //return BadRequest();
                return NotFound("A Cidade não foi localizada!");
            }

            try
            {
                if (await _repositoryCidade.RemoverCidade(id))
                {
                    return Ok("A Cidade removida com sucesso!");
                }
                else
                {
                    return BadRequest("Não foi possível remover a Cidade.");
                }

            }
            catch (Exception)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao remover o registro.!");
            }
        }
    }
}
