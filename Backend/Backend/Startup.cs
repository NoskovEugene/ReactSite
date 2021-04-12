using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Common.Configurations.AppConfiguration;
using Backend.Common.Extensions.ConfigurationExtensions;
using Backend.Common.Extensions.StringExtensions;
using Backend.DAL;
using Backend.DAL.Models;
using Backend.DAL.Repositories;
using Backend.Modules;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private JwtConfiguration JwtConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureConfigurations(services);
            RegisterDb(services);
            ConfigureCorsAndAuthentication(services);
            services.AddAutoMapper(x => x.AddProfile<AutomapperProfile>());
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() {Title = "My API", Version = "v1"});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Jwt authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
            });
        }

        #region Configs

        private void ConfigureCorsAndAuthentication(IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddDefaultPolicy(config =>
                {
                    config.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .AllowAnyHeader();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = JwtConfiguration.Issuer,
                            ValidateAudience = true,
                            ValidAudience = JwtConfiguration.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = JwtConfiguration.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true
                        };
                    });
        }

        private void ConfigureConfigurations(IServiceCollection services)
        {
            this.JwtConfiguration = Configuration.GetSection("JwtOptions").Get<JwtConfiguration>();
            services.Configure<JwtConfiguration>(Configuration.GetSection("JwtOptions"));
            services.Configure<CookieConfiguration>(Configuration.GetSection("CookieOptions"));
            services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>();
        }

        private void RegisterDb(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            var path = Path.GetDirectoryName(
                connection.Replace("Data source=", string.Empty, StringComparison.InvariantCultureIgnoreCase));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            services.AddDbContext<SiteDbContext>(options =>
            {
                options.UseSqlite(
                           connection, x =>
                               x.MigrationsAssembly("Backend.DAL"))
                       .UseLazyLoadingProxies();
            });

            services.AddTransient<IRepositoryBase<User>, RepositoryBase<User>>();
        }

        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cookieName = Configuration.GetSection("CookieOptions").GetValue<string>("CookieName");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My api v1"));
            }

            app.Use(async (context, next) =>
            {
                var headers = context.Request.Headers.ToList();
                if (context.Request.Cookies.TryGetValue(cookieName, out var cookie))
                {
                    context.Request.Headers.Add(
                        new KeyValuePair<string, StringValues>("Authorization", new StringValues($"Bearer {cookie}"))
                    );
                }

                await next();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}