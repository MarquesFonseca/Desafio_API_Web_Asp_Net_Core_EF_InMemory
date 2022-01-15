using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Repository
{
    public interface IRepositoryCidade
    {
        public Task<List<Cidade>> GetCidades();

        public Task<List<Cidade>> GetCidadesOrderByDesc();

        public Task<Cidade> GetCidadeById(int id);

        public Task<Cidade> GetCidadeByModel(Cidade model);

        public Task<List<Cidade>> GetCidadeByNomeOrEstado(string nome);
        public Task<bool> SeExisteCidade(int id);

        public Task<Cidade> CadastrarCidade(Cidade model);

        public Task<Cidade> AlterarCidade(Cidade model);

        public Task<bool> RemoverCidade(int id);
    }
}
