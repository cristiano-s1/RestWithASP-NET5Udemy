using Serilog;
using System;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Net.Http.Headers;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWithASPNETUdemy.Business.Implementation;
using RestWithASPNETUdemy.Repository.Implementation;
using RestWithASPNETUdemy.Repository.Generic;

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestWithASPNETUdemy", Version = "v1" });
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

            app.UseRouting();

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
