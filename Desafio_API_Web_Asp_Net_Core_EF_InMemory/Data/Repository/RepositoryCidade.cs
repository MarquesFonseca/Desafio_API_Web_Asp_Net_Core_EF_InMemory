using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Repository;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Helpers;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Utils;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Interface;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Repository
{
    public class RepositoryCidade : IRepositoryCidade, IDisposable
    {
        private readonly DataContext _dataContext;
        public RepositoryCidade(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }        

        public async Task<List<Cidade>> GetCidades()
        {
            var cidades = await _dataContext.Cidades.AsNoTracking().ToListAsync();
            return cidades.FormatarCampos();
        }

        public async Task<List<Cidade>> GetCidadesOrderByDesc()
        {
            var cidades = await _dataContext.Cidades.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            return cidades.FormatarCampos();
        }

        public async Task<Cidade> GetCidadeById(int id)
        {
            var cidade = await _dataContext.Cidades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return cidade.FormatarCampos();
        }

        public async Task<Cidade> GetCidadeByModel(Cidade model)
        {
            var cidade = await _dataContext.Cidades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            return cidade.FormatarCampos();
        }

        public async Task<List<Cidade>> GetCidadeByNomeOrEstado(string nome)
        {
            var cidade = await _dataContext.Cidades
                .AsNoTracking()
                .Where(x =>
                x.Nome.ToString().ToUpper().RemoveAcentos().Contains(nome.ToUpper().RemoveAcentos()) ||
                x.EstadoUF.ToString().ToUpper().Contains(nome.ToUpper()))
                .ToListAsync();

            return cidade.FormatarCampos();
        }

        public async Task<bool> SeExisteCidade(int id)
        {
            bool existe = await _dataContext.Cidades.AnyAsync(x => x.Id == id);
            return existe;
        }

        public async Task<Cidade> CadastrarCidade(Cidade model)
        {
            //System.Guid g = System.Guid.NewGuid();
            //model.Id = g.ToString();
            _dataContext.Cidades.Add(model);
            await _dataContext.SaveChangesAsync();
            return await GetCidadeByModel(model);
        }

        public async Task<Cidade> AlterarCidade(Cidade model, int id)
        {
            try
            {
                model.Id = id;
                _dataContext.Entry(model).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await GetCidadeByModel(model);
        }

        public async Task<bool> RemoverCidade(int id)
        {
            try
            {
                _dataContext.Cidades.Remove(await GetCidadeById(id));
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


















        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // descartar recursos quando necessário
            }
        }
        #endregion
    }
}
