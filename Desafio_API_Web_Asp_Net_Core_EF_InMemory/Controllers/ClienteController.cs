using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Interface;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Helpers;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IRepositoryCliente _repositoryCliente;
        private readonly IRepositoryCidade _repositoryCidade;
        public ClienteController(IRepositoryCliente repositoryCliente, IRepositoryCidade repositoryCidade)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryCidade = repositoryCidade;
        }

        /// <summary>
        /// Obter todos os Clientes.
        /// </summary>
        /// <response code="200">A lista de clientes foi obtida com sucesso.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de clientes.</response>
        /// <returns>Retorna uma lista de clientes.</returns> 
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetClients()
        {
            return Ok(await _repositoryCliente.GetClientes());
        }

        /// <summary>
        /// Obter um cliente específico pelo seu Id.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <response code="200">O cliente foi obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado cliente com Id especificado.</response>
        /// <response code="500">Ocorreu um erro ao obter o usuário.</response>
        /// <returns>Retorna um clientes.</returns> 
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            return Ok(await _repositoryCliente.GetClienteById(id));
        }

        /// <summary>
        /// Obter um cliente pesquisando pelo seu nome ou parte do nome.
        /// </summary>
        /// <param name="nomeCliente">Nome ou parte do nome desejado na busca</param>
        /// <response code="200">Os clientes com a busca foram obtidos com sucesso.</response>
        /// <response code="404">Não foi encontrado cliente com nome buscado.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de clientes.</response>
        /// <returns>Retorna um cliente.</returns> 
        [HttpGet]
        [Route("pesquisar/nome/{nomeCliente}")]
        public async Task<ActionResult<List<Cliente>>> GetClienteByParteNomeCompleto(string nomeCliente)
        {
            return Ok(await _repositoryCliente.GetClienteByParteNomeCompleto(nomeCliente));
        }

        /// <summary>
        /// Retorna todos os clientes de uma cidade específica
        /// </summary>
        /// <param name="CidadeId">Id da cidade desejada.</param>
        /// <response code="200">Os clientes foram obtidos com sucesso.</response>
        /// <response code="404">Não foi encontrado clientes com Id da cidade informada.</response>
        /// <response code="500">Ocorreu um erro ao obter ao clientes da cidade informada.</response>
        /// <returns>Retorna uma lista de clientes de uma mesma cidade.</returns>       
        [HttpGet]
        [Route("pesquisar/cidade/{CidadeId:int}")]
        public async Task<ActionResult<List<Cliente>>> GetClientesByCidadeId(int CidadeId)
        {
            return Ok(await _repositoryCliente.GetClientesByCidadeId(CidadeId));
        }

        /// <summary>
        /// Cadastrar um cliente.
        /// </summary>
        /// <param name="cliente">Modelo do cliente pelo body.</param>
        /// <response code="200">O cliente foi cadastrado com sucesso.</response>
        /// <response code="400">O modelo do cliente enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro ao cadastrar o cliente.</response>
        /// <returns>Retorna o Cliente recém inserido.</returns>
        /// <returns>Após gravar o novo cliente, retorna ele mesmo.</returns> 
        [HttpPost]
        [Route("novo")]
        public async Task<ActionResult<Cliente>> CreateCidade([FromBody] Cliente cliente)
        {
            if (FormatacaoData.VerificaDataSeValida(cliente.DataNascimento) == false)
                return NotFound("Não foi possível gravar os dados do cliente. \nCidade Inválida.\nInform no fomrato 'ano-mes-dia'");

            bool existeACidadeInformada = await _repositoryCidade.SeExisteCidade(cliente.CidadeId);
            if (existeACidadeInformada == false)//vai inserir um novo cliente se existir a cidade informada.
                return NotFound("Não foi possível gravar os dados do cliente. Cidade Inválida.");

            try
            {
                if (ModelState.IsValid)
                {
                    if (await _repositoryCliente.CadastrarCliente(cliente))
                    {

                        //var clienteJaCadastrado = await _repositoryCliente.GetClienteById(_cliente.Id);
                        cliente.Cidade = await _repositoryCidade.GetCidadeById(cliente.CidadeId);
                        return cliente;
                    }
                    else
                    {
                        return BadRequest("Não foi possível cadastrar a cidade informada.");
                    }
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
        /// Alterar cliente.
        /// </summary> 
        /// <param name="id">Id do Cliente.</param>
        /// <param name="cliente">Modelo do Cliente.</param>
        /// <response code="200">O Cliente foi alterado com sucesso.</response>
        /// <response code="400">O modelo do cliente enviado é inválido.</response>
        /// <response code="404">Não foi encontrado cliente com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao alterar o cliente.</response>
        /// <returns>Retorna o cliente já alterado.</returns> 
        [HttpPut]
        [Route("alterar/{id:int}")]
        public async Task<ActionResult<Cliente>> AlterarCliente([FromBody] Cliente cliente, int id)
        {
            #region verifica se a data é válida
            if (FormatacaoData.VerificaDataSeValida(cliente.DataNascimento) == false)
                return NotFound("Não foi possível gravar os dados do cliente. \nCidade Inválida.\nInform no fomrato 'ano-mes-dia'");
            #endregion

            #region verifica se a cidade informada existe
            bool existeACidadeInformada = await _repositoryCidade.SeExisteCidade(cliente.CidadeId);
            if (existeACidadeInformada == false)//vai alterar um cliente se existir a cidade informada.
                return NotFound("Não foi possível gravar os dados do cliente. Cidade Inválida.");
            #endregion

            #region Verifica se o cliente existe no banco
            bool existe = await _repositoryCliente.SeExisteCliente(id);
            if (!existe)
                return NotFound("Cliente não encontrado!");
            #endregion

            try
            {
                await _repositoryCliente.AlterarCliente(cliente, id);
                cliente.Cidade = await _repositoryCidade.GetCidadeById(cliente.CidadeId);
                return cliente;
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound("Ocorreu um erro ao alterar o cliente atual. Não foi possível retornar o cliente.");
            }
        }

        /// <summary>
        /// Remover usuário.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <response code="200">O cliente foi deletado com sucesso.</response>
        /// <response code="404">Não foi encontrado cliente com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao remover o cliente.</response>
        /// <returns>Retorna um resultado da remoção.</returns> 
        [HttpDelete]
        [Route("remover/{id:int}")]
        public async Task<IActionResult> RemoverCliente(int id)
        {

            var clienteAtual = await _repositoryCliente.GetClienteById(id);
            if (clienteAtual == null)
            {
                //return BadRequest();
                return NotFound("O Cliente não foi localizada!");
            }

            try
            {
                if (await _repositoryCliente.RemoverCliente(id))
                    return Ok("Cliente removida com sucesso!");
                else
                    return NotFound("Ocorreu um erro ao remover o registro.");
            }
            catch (Exception)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                    "Ocorreu um erro ao remover o registro.!");
            }
        }
    }
}
