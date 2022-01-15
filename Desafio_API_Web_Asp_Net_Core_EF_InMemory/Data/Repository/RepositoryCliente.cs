using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Repository;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Helpers;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Services
{
    public class RepositoryCliente : IRepositoryCliente, IDisposable
    {
        private readonly DataContext _dataContext;

        public RepositoryCliente(DataContext dataContext)
        {
            _dataContext = dataContext;

            //é o método responsável por garantir que o squema com o contexxto esteja criado. 
            //caso não exista, o banco de dados e todo o seu esquema são criados
            //e também garante que seja compatível com o modelo para este contexto.
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<Cliente>> GetClientes()
        {
            var clientes = await _dataContext.Clientes.AsNoTracking().ToListAsync();

            return clientes.FormatarCampos();
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
