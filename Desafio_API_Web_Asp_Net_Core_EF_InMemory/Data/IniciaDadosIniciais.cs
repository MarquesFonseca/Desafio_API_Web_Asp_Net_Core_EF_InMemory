using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data
{
    public static class IniciaDadosIniciais
    {
        public static void CarregaCidade(ModelBuilder modelBuilder)
        {
            //carrega cidades
            modelBuilder.Entity<Cidade>().HasData(
                new Cidade()
                {
                    Id = 1,
                    Nome = "Palmas",
                    EstadoUF = "TO"
                },
                new Cidade()
                {
                    Id = 2,
                    Nome = "Goiânia",
                    EstadoUF = "GO"
                },
                new Cidade()
                {
                    Id = 3,
                    Nome = "São Luis",
                    EstadoUF = "MA"
                },
                new Cidade()
                {
                    Id = 4,
                    Nome = "Terezina",
                    EstadoUF = "PI"
                },
                new Cidade()
                {
                    Id = 5,
                    Nome = "Salvador",
                    EstadoUF = "BA"
                },
                new Cidade()
                {
                    Id = 6,
                    Nome = "Belém",
                    EstadoUF = "PA"
                },
                new Cidade()
                {
                    Id = 7,
                    Nome = "Rio Branco",
                    EstadoUF = "AC"
                },
                new Cidade()
                {
                    Id = 8,
                    Nome = "Amazonas",
                    EstadoUF = "AM"
                },
                new Cidade()
                {
                    Id = 9,
                    Nome = "Cuiabá",
                    EstadoUF = "MT"
                },
                new Cidade()
                {
                    Id = 10,
                    Nome = "Distrito Federal",
                    EstadoUF = "DF"
                }
            );
        }

        public static void CarregaClientes(ModelBuilder modelBuilder)
        {
            //carrega cidades
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente()
                {
                    Id = 1,
                    NomeCompleto = "Marques Silva Fonseca",
                    Sexo = "M",
                    DataNascimento = new DateTime(1986, 1, 2),
                    Idade = Utils.FormatacaoData.RetornaIdade(new DateTime(1986, 1, 2)),
                    CidadeId = 1,
                    DataCadastro = DateTime.Now
                },
                new Cliente()
                {
                    Id = 2,
                    NomeCompleto = "Maria Silva Rocha",
                    Sexo = "F",
                    DataNascimento = new DateTime(1960, 1, 31),
                    Idade = Utils.FormatacaoData.RetornaIdade(new DateTime(1960, 1, 31)),
                    CidadeId = 1,
                    DataCadastro = DateTime.Now
                },
                new Cliente()
                {
                    Id = 3,
                    NomeCompleto = "Roque Alves Fonseca",
                    Sexo = "M",
                    DataNascimento = new DateTime(1961, 8, 17),
                    Idade = Utils.FormatacaoData.RetornaIdade(new DateTime(1961, 8, 17)),
                    CidadeId = 2,
                    DataCadastro = DateTime.Now
                },
                new Cliente()
                {
                    Id = 4,
                    NomeCompleto = "Roselene Silva Fonseca",
                    Sexo = "F",
                    DataNascimento = new DateTime(1984, 2, 10),
                    Idade = Utils.FormatacaoData.RetornaIdade(new DateTime(1984, 2, 10)),
                    CidadeId = 2,
                    DataCadastro = DateTime.Now
                },
                new Cliente()
                {
                    Id = 5,
                    NomeCompleto = "Lauanda da Silva Ferreira Fonseca",
                    Sexo = "F",
                    DataNascimento = new DateTime(1985, 5, 12),
                    Idade = Utils.FormatacaoData.RetornaIdade(new DateTime(1985, 5, 12)),
                    CidadeId = 2,
                    DataCadastro = DateTime.Now
                },
                new Cliente()
                {
                    Id = 6,
                    NomeCompleto = "Maitê Marques Ferreira Fonseca",
                    Sexo = "F",
                    DataNascimento = new DateTime(2021, 10, 27),
                    Idade = Utils.FormatacaoData.RetornaIdade(new DateTime(2021, 10, 27)),
                    CidadeId = 2,
                    DataCadastro = DateTime.Now
                }
            ); 
        }
    }
}
