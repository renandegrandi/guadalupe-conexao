using AutoMapper;
using Guadalupe.Conexao.Api.Extensions;
using Guadalupe.Conexao.Api.Infrastructure.Config;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Guadalupe.Conexao.Api
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
            //TODO: adicionar valida��o de environment.
            var isDevelopment = true;

            services.AddRepositories(Configuration, isDevelopment);
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.Configure<SmtpConfig>(Configuration.GetSection(SmtpConfig.Key));
            services.Configure<AuthenticationConfig>(Configuration.GetSection(AuthenticationConfig.Key));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer((options) =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["Authentication:Jwt:SymmetricKey"]);

                    //options.Authority = Configuration["Authentication:Jwt:Authority"];
                    //options.Audience = Configuration["Authentication:Jwt:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services
                .AddAutoMapper(typeof(Startup));

            //FirebaseAdmin.FirebaseApp.Create(new FirebaseAdmin.AppOptions
            //{
            //    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase.json")),
            //});

            services.AddApplicationInsightsTelemetry(Configuration.GetSection("ApplicationInsights"))
                .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
                {
                    module.EnableSqlCommandTextInstrumentation = true;
                    o.EnableAdaptiveSampling = false;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
