using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blurtle.Application;
using Blurtle.Infrastructure;
using Blurtle.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FluentValidation;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Blurtle.Domain;
using Microsoft.AspNetCore.Http;

namespace Blurtle.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Set up the authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts => {
                opts.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = Configuration["AuthenticationToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthenticationToken:Secret"]))
                };

                opts.Events = new JwtBearerEvents();
                opts.Events.OnTokenValidated = async (c) => {
                    // Figure out the user ID the token belongs to.
                    Claim subjectClaim = c.Principal.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
                    int userId = Convert.ToInt32(subjectClaim.Value);

                    // Retrieve the user
                    IUserRepo userRepo = c.HttpContext.RequestServices.GetService<IUserRepo>();
                    c.HttpContext.User = await userRepo.FindById(userId);
                };
            });


            services.ConfigurePoco<IDatabaseConfig, DatabaseConfig>(Configuration.GetSection("Database"));
            services.ConfigurePoco<IAuthenticationTokenConfig, AuthenticationTokenConfig>(Configuration.GetSection("AuthenticationToken"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IAuthenticationTokenHandler, JsonWebTokenHandler>();
            services.AddSingleton<IDatabase, MySqlDatabase>();
            services.AddTransient<IUserRepo, UserRepo>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddTransient<FindUserByUsernameInteractor>();
            services.AddTransient<LoginUserInteractor>();
            services.AddTransient<RegisterUserInteractor>();
            services.AddTransient<UpdateUserInteractor>();
            services.AddTransient<UserPasswordUpdater>();

            services.AddTransient<AbstractValidator<RegisterUserParams>, RegisterUserValidator>();
            services.AddTransient<AbstractValidator<UpdateUserParams>, UserUpdateValidator>();
            services.AddTransient<AbstractValidator<UserPasswordUpdateParams>, UserPasswordValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseAuthentication();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
