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

            // TODO adicionando o json para serializar as amarrações
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve) ;

            /*
             *  AllowAnyOrigin: Recebe requisições de qualquer origem, caso você queira receber requisições de uma origem especifica, é somente passar a URL da origem no método WithOrigins
                AllowAnyMethod: Recebe requisições de qualquer método ex: POST, PUT, DELETE, GET e etc. Também pode restringir somente para métodos específicos, utilizando o método WithMethods
                AllowAnyHeader: Recebe requisições com qualquer tipo de cabeçalho ex: Cache-Control, Content-Language. Também pode restringir somente cabeçalhos específicos, utilizando o método WithHeader
                AllowCredentials: Recebe requisições com qualquer tipo de credencial entre origens, no cabeçalho do tipo: Access-Control-Allow-Credentials
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


            // Adicionando as inversões de controle
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
            // TODO adicionando o CORS especificando a política que criamos nas services
            // TODO obrigatoriamente precisa estar entre o useRouting e o useEndpoints
            // UseRouting Adiciona correspondência de rota ao pipeline de middleware.Esse middleware analisa o conjunto de pontos de extremidade definidos no aplicativo e seleciona a melhor correspondência com base na solicitação.
            // UseEndpoints Adiciona a execução de ponto de extremidade ao pipeline de middleware. Ele executa o delegado associado ao ponto de extremidade selecionado.
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
