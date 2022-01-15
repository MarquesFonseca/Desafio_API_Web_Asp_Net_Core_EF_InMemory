using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Interface;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        /// Obter todos os cidades.
        /// </summary>
        /// <response code="200">A lista de cidades foi obtida com sucesso.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de cidades.</response>
        /// <returns>Retorna uma lista de cidades.</returns> 
        [HttpGet]//definindo o verbo utilizado. Se não colocar nada, por padrão ele assume o GET
        [Route("")]//rota vazia, ou seja, será a mesma rota definida no controller
        public async Task<ActionResult> GetCity()
        {
            return Ok(await _repositoryCidade.GetCidades());
        }

        /// <summary>
        /// Obter uma cidade específica pelo seu Id.
        /// </summary>
        /// <param name="id">Id da cidade.</param>
        /// <response code="200">O cliente foi obtido com sucesso.</response>
        /// <response code="404">Não foi encontrada cidade com Id especificado.</response>
        /// <response code="500">Ocorreu um erro ao obter o usuário.</response>
        /// <returns>Retorna uma cidade.</returns> 
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cidade>> GetCidadeById(int id)
        {
            return Ok(await _repositoryCidade.GetCidadeById(id));
        }

        /// <summary>
        /// Obter uma cidade pesquisando pelo seu nome ou parte do nome.
        /// </summary>
        /// <param name="nome">Nome ou parte do nome desejado na busca</param>
        /// <response code="200">As cidades com a busca foram obtidos com sucesso.</response>
        /// <response code="404">Não foi encontrada cidade com nome buscado.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de cidades.</response>
        /// <returns>Retorna uma cidade.</returns> 
        [HttpGet]
        [HttpHead]
        [Route("pesquisar/{nome}")]
        public async Task<ActionResult<List<Cidade>>> GetCidadeByNome(string nome)
        {
            return Ok(await _repositoryCidade.GetCidadeByNomeOrEstado(nome));
        }

        /// <summary>
        /// Cadastrar uma nova cidade.
        /// </summary>
        /// <param name="cidade">Modelo do objeto cidade pelo body.</param>
        /// <response code="200">A cidade foi cadastrado com sucesso.</response>
        /// <response code="400">O modelo da cidade enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro ao cadastrar a cidade.</response>
        /// <returns>Retorna a cidade recém inserido.</returns>
        /// <returns>Após gravar uma nova cidade, retorna ele mesmo.</returns> 
        [HttpPost]
        [Route("novo")]
        public async Task<ActionResult<Cidade>> CadastrarCidade([FromBody] Cidade cidade)
        {
            if (cidade is null)
            {
                throw new ArgumentNullException(nameof(cidade));
            }

            if (ModelState.IsValid)
            {
                return Ok(await _repositoryCidade.CadastrarCidade(cidade));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Alterar Cidade.
        /// </summary> 
        /// <param name="id">Id da cidade.</param>
        /// <param name="cidade">Modelo da cidade.</param>
        /// <response code="200">A cidade foi alterado com sucesso.</response>
        /// <response code="400">O modelo da cidade enviado é inválido.</response>
        /// <response code="404">Não foi encontrada cidade com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao alterar a cidade.</response>
        /// <returns>Retorna uma cidade já alterado.</returns> 
        [HttpPut]
        [Route("alterar/{id:int}")]
        public async Task<ActionResult<Cidade>> AlterarCidade([FromBody] Cidade cidade, int id)
        {
            bool existe = await _repositoryCidade.SeExisteCidade(id);
            if (!existe)
                return NotFound("Não foi possível gravar os dados da cidade. Cidade Inválida.");

            try
            {
                await _repositoryCidade.AlterarCidade(cidade, id);
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
        /// Remover Cidade.
        /// </summary>
        /// <param name="id">Id da Cidade.</param>
        /// <response code="200">A Cidade foi removida com sucesso.</response>
        /// <response code="404">Não foi encontrada cidade com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao remover a cidade.</response>
        /// <returns>Retorna um resultado da remoção.</returns> 
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
