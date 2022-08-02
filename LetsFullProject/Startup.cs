using LetsFullProject.Configurations;
using LetsFullProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

namespace LetsFullProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<LetsFullProjectContext>(
              options => options.
              UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // add o contexto 
            services.AddDbContext<LetsFullProjectContext>();
            // adicionando as controllers

            // TODO adicionando o json para serializar as amarra��es
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve) ;

            /*
             *  AllowAnyOrigin: Recebe requisi��es de qualquer origem, caso voc� queira receber requisi��es de uma origem especifica, � somente passar a URL da origem no m�todo WithOrigins
                AllowAnyMethod: Recebe requisi��es de qualquer m�todo ex: POST, PUT, DELETE, GET e etc. Tamb�m pode restringir somente para m�todos espec�ficos, utilizando o m�todo WithMethods
                AllowAnyHeader: Recebe requisi��es com qualquer tipo de cabe�alho ex: Cache-Control, Content-Language. Tamb�m pode restringir somente cabe�alhos espec�ficos, utilizando o m�todo WithHeader
                AllowCredentials: Recebe requisi��es com qualquer tipo de credencial entre origens, no cabe�alho do tipo: Access-Control-Allow-Credentials
             * */

            // TODO GLOBALMENTE
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        /*.SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowCredentials()*/
                        );
            });





            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LetsFullProject", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Adicionando as invers�es de controle
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LetsFullProject v1"));
            }
          
            app.UseHttpsRedirection();
            app.UseRouting();
            // TODO adicionando o CORS especificando a pol�tica que criamos nas services
            // TODO obrigatoriamente precisa estar entre o useRouting e o useEndpoints
            // UseRouting Adiciona correspond�ncia de rota ao pipeline de middleware.Esse middleware analisa o conjunto de pontos de extremidade definidos no aplicativo e seleciona a melhor correspond�ncia com base na solicita��o.
            // UseEndpoints Adiciona a execu��o de ponto de extremidade ao pipeline de middleware. Ele executa o delegado associado ao ponto de extremidade selecionado.
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            Factory.RegisterServices(services);
        }
    }
}
