using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementation;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;
using RestWithASPNETUdemy.Repository.Implementation;
using Serilog;
using System;
using System.Collections.Generic;

namespace RestWithASPNETUdemy
{
    public class Startup
    {      
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region CORS
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

            }));
            #endregion

            services.AddControllers();

            #region CONNECTION DATA BASE
            //SQL SERVER
            var connection = Configuration["SQLServerConnection:SQLServerConnectionStrings"];
            services.AddDbContext<RestFullContext>(options => options.UseSqlServer(connection));
            #endregion

            #region MIGRATIONS
            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }
            #endregion

            #region SUPORT JSON AND XML API
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();
            #endregion

            #region VERSIONING API
            services.AddApiVersioning();
            #endregion

            #region DEPENDECY INJECTION
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();

            //Generic
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            #endregion

            #region SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo { 
                        Title = "REST API's RESTFul do 0 à Azure com ASP.NET Core 5 e Docker.", 
                        Version = "v1",
                        Description = "API RESTfull desenvolvida no curso 'REST API's RESTFul do 0 à Azure com ASP.NET Core 5 e Docker'.",
                        Contact = new OpenApiContact
                        {
                            Name = "Cristiano Campos de Souza",
                            Url = new Uri("https://github.com/cristiano-s1")
                        }
                    });
            });
            #endregion

        }

      
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestWithASPNETUdemy v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region CORS
            app.UseCors();
            #endregion

            #region SWAGGER
            //Responsavel por gerar o Json com a documentação
            app.UseSwagger();

            //Responsavel por gerar uma pagina html 
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API's RESTFul do 0 à Azure com ASP.NET Core 5 e Docker - v1");
            });

            
            var option = new RewriteOptions();

            //Redirecionar para página do swagger
            option.AddRedirect("^$", "swagger");

            //Configurar web page
            app.UseRewriter(option);
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }

        #region MÉTODO MIGRATION RECEIVE CONNECTION STRING
        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new SqlConnection(connection);

                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
        #endregion

    }
}
