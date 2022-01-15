﻿using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
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
    public class RepositoryCliente : IRepositoryCliente, IDisposable
    {
        private readonly DataContext _dataContext;

        public RepositoryCliente(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public async Task<List<Cliente>> GetClientes()
        {
            var clientes = await _dataContext.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .ToListAsync();

            return clientes.FormatarCampos();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            var cliente = await _dataContext.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return cliente.FormatarCampos();
        }

        public async Task<List<Cliente>> GetClienteByParteNomeCompleto(string nomeCliente)
        {
            var cliente = await _dataContext.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .Where(x => x.NomeCompleto.ToUpper().RemoveAcentos().Contains(nomeCliente.ToUpper().RemoveAcentos()))
                .ToListAsync();

            return cliente.FormatarCampos();
        }

        public async Task<List<Cliente>> GetClientesByCidadeId(int CidadeId)
        {
            var clientes = await _dataContext.Clientes
                .Include(x => x.Cidade)
                .AsNoTracking()
                .Where(x => x.CidadeId == CidadeId)
                .ToListAsync();

            return clientes.FormatarCampos();
        }

        public async Task<bool> SeExisteCliente(int id)
        {
            bool existe = await _dataContext.Clientes.AnyAsync(x => x.Id == id);
            return existe;
        }

        public async Task<bool> CadastrarCliente(Cliente _cliente)
        {
            try
            {
                _cliente.FormatarCampos();
                _cliente.DataCadastro = DateTime.Now;

                _dataContext.Clientes.Add(_cliente);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Cliente> AlterarCliente(Cliente _cliente)
        {
            try
            {

                _cliente.FormatarCampos();

                var clienteAntesGravar = await GetClienteById(_cliente.Id);
                _cliente.DataCadastro = clienteAntesGravar.DataCadastro;

                //_cliente.DataCadastro = await GetClienteById(_cliente.Id).
                _dataContext.Entry(_cliente).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();

                return _cliente;
            }
            catch (Exception ex)
            {
                return _cliente;
            }
        }

        public async Task<bool> RemoverCliente(int id)
        {
            try
            {
                _dataContext.Clientes.Remove(await GetClienteById(id));
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
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
