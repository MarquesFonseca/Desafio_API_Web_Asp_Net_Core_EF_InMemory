using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;

namespace Desafio_API_Web_Asp_Net_Core_EF_InMemory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Informar para nossa aplicação que temos um DataContext,
        /// e que vamos trabalhar com o Entity Framework,
        /// e que usaremos nosso banco de dados em memória.
        /// Estamos dando o nome para o nosso banco de dados de "DataBase"
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Deixar nosso DatContext disponível através do "AddScoped", que é a nossa injeção de dependências do .net Core.
            //Significa que, se em algum lugar da nossa aplicação solicitar o DataContext, vai deixar em memória,
            //onde não cria uma nova versão toda vez que requisitar, ou seja, não vai abri uma nova conexão no banco.
            //e assim que a requisição terminar, a aplicação vai destruir o DataContext, para não deixar vestígios na memória.
            services.AddScoped<DataContext, DataContext>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
