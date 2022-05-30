using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using TweetAppAPI.DatabaseConnection;
using TweetAppAPI.Repository;
using TweetAppAPI.Service;

namespace TweetAppAPI
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
            Validatetoken(Configuration, services);

            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<ITweetService, TweetService>();
            services.AddSingleton<ITweetRepository, TweetRepository>();

            services.AddCors();

            services.AddControllers();

            services.AddApiVersioning(config => {
                config.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TweetAppAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                              new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}

                        }
                    });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TweetAppAPI v1"));

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void Validatetoken(IConfiguration configuration, IServiceCollection services)
        {
            var userSecretKey = configuration["Token:SecretKey"];
            var byteArray = Encoding.ASCII.GetBytes(userSecretKey);
            var userSymmetricSecurityKey = new SymmetricSecurityKey(byteArray);
            var userTokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = configuration["Token:Issuer"],

                ValidateAudience = true,
                ValidAudience = configuration["Token:Audience"],


                ValidateIssuerSigningKey = true,
                IssuerSigningKey = userSymmetricSecurityKey,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(u => {
                u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(u => u.TokenValidationParameters = userTokenValidationParameters);
        }
    }
}
