using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Webapi.Infraestructure.Repositorys.Abstract;
using Webapi.Infraestructure.Repositorys.Concrete;
using Webapi.Domain.Services.Abstract;
using Webapi.Domain.Services.Concrete;
using Webapi.Infraestructure.Config;
using Microsoft.EntityFrameworkCore;
using System;

namespace Webapi
{
    public class Startup
    {
        readonly string corsUDS = "_corsFORTES";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<SimuladorContext>(opt => opt.UseSqlServer(connectionString));
            
            services.AddTransient<ICompraRepository, CompraRepository>();
            services.AddTransient<IParcelaRepository, ParcelaRepository>();
            services.AddTransient<ICompraService, CompraService>();
            services.AddTransient<IParcelaService, ParcelaService>();           

            services.AddCors(options =>
            {   
                options.AddPolicy(corsUDS,
                builder =>
                {
                     builder.AllowAnyOrigin() 
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                });               
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "FORTES API", Version = "v1" });               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FORTES API V1");
            });
            app.UseCors(corsUDS);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
