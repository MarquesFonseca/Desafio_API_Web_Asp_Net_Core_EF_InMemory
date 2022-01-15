using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data
{
    /// <summary>
    /// Classe DataContext (representação do nosso banco de dados, que usaremos o banco em memória.)
    /// Erda o DbContext
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Por aqui se usa a conectionString(conexão com bancos, SqlServer, Oracle, MySql, etc)
        /// </summary>
        /// <param name="options">Neste caso, não repassaremos nenhuma ação para o DataContext.</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// Nossa coleção de tabelas do nosso banco de dados.
        /// Aqui definimos quais tabelas vamos usar.
        /// </summary>
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //carrega cidades
            IniciaDadosIniciais.CarregaCidade(modelBuilder);

            //carrega Clientes
            IniciaDadosIniciais.CarregaClientes(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
