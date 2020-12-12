using AutoMapper;
using Guadalupe.Conexao.Backoffice.Mappings;
using Guadalupe.Conexao.Backoffice.Repository;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Guadalupe.Conexao.Backoffice
{
    public class Startup
    {
        public static readonly AuthenticationDto authentication = new AuthenticationDto
        {
            GrantType = GrantTypes.password,
            Username = "missaoguadalupe@hotmail.com",
            Password = "AAAA"
        };
        public const string ApiUrl = "http://localhost:49981/";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddScoped<INoticeRepository, NoticeRepository>();
            services.AddScoped<INoticeService, NoticeService>();
            services.AddScoped<IUserService, UserService>();

            services.AddHttpClient("guadalupe-conexao-api", (c) =>
            {
                c.BaseAddress = new Uri(ApiUrl);
            });

            services
                .AddAutoMapper(new List<Type> { 
                    typeof(NoticeProfiler)
                }.ToArray());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
