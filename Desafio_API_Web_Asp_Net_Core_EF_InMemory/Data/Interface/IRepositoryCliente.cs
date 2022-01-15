using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Interface
{
    public interface IRepositoryCliente
    {
        public Task<List<Cliente>> GetClientes();

        public Task<Cliente> GetClienteById(int id);

        public Task<List<Cliente>> GetClienteByParteNomeCompleto(string nomeCliente);

        public Task<List<Cliente>> GetClientesByCidadeId(int CidadeId);

        public Task<bool> SeExisteCliente(int id);

        public Task<bool> CadastrarCliente(Cliente _cliente);

        public Task<Cliente> AlterarCliente(Cliente _cliente);

        public Task<bool> RemoverCliente(int id);

    }
}
