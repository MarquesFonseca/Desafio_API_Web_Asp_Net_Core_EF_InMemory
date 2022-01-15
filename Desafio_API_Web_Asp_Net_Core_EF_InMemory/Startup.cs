using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Repository;
using Desafio_API_Web_Asp_Net_Core_EF_InMemory.Data.Interface;

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
        /// Informar para nossa aplica��o que temos um DataContext,
        /// e que vamos trabalhar com o Entity Framework,
        /// e que usaremos nosso banco de dados em mem�ria.
        /// Estamos dando o nome para o nosso banco de dados de "DataBase"
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DataBase"));

            //Deixar nosso DatContext dispon�vel atrav�s do "AddScoped", que � a nossa inje��o de depend�ncias do .net Core.
            //Significa que, se em algum lugar da nossa aplica��o solicitar o DataContext, vai deixar em mem�ria,
            //onde n�o cria uma nova vers�o toda vez que requisitar, ou seja, n�o vai abri uma nova conex�o no banco.
            //e assim que a requisi��o terminar, a aplica��o vai destruir o DataContext, para n�o deixar vest�gios na mem�ria.
            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<IRepositoryCidade, RepositoryCidade>();
            services.AddTransient<IRepositoryCliente, RepositoryCliente>();
            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Compass.Uol",
                    Description = "API Asp NET Core 3.1 Entity Framework InMemory",
                    TermsOfService = new Uri("https://github.com/MarquesFonseca/Desafio_API_Web_Asp_Net_Core_EF_InMemory")
                });
            });
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

            app.UseSwagger();

           app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
