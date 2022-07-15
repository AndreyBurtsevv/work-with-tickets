using BoxOffice.Core.Data;
using BoxOffice.Core.Data.Mapper;
using BoxOffice.Core.Middleware;
using BoxOffice.Core.Services.Implementations;
using BoxOffice.Core.Services.Interfaces;
using BoxOffice.Core.Services.Provaiders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sieve.Services;
using System;
using System.Text;
using FluentValidation.AspNetCore;
using BoxOffice.Core.Data.Validators;

namespace BoxOffice
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));
            services.AddAutoMapper(typeof(MappingEntity));
            services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<CreateSpectacleValidator>();
                config.RegisterValidatorsFromAssemblyContaining<SpectacleDtoValidator>();
            });
            services.AddHttpContextAccessor();
            services.AddScoped<SieveProcessor>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISpectacleService, SpectacleService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddControllers()
             .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddFluentValidation(config =>
             {
                 config.RegisterValidatorsFromAssemblyContaining<CreateSpectacleValidator>();
                 config.RegisterValidatorsFromAssemblyContaining<SpectacleDtoValidator>();
             }); ;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Token:Secret"])),
                    ValidAudience = Configuration["Token:Audience"],
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BoxOffice", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
                       Array.Empty<string>()
                      }
                 });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BoxOffice v1"));
            }

            app.UseMiddleware();
            app.UseHttpsRedirection();
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
