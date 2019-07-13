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

namespace Blurtle.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.ConfigurePoco<IDatabaseConfig, DatabaseConfig>(Configuration.GetSection("Database"));
            services.ConfigurePoco<IAuthenticationTokenConfig, AuthenticationTokenConfig>(Configuration.GetSection("AuthenticationToken"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IDatabase, MySqlDatabase>();
            services.AddTransient<IUserRepo, UserRepo>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            services.AddSingleton<IAuthenticationTokenHandler, JsonWebTokenHandler>();

            services.AddTransient<FindUserByUsernameInteractor>();
            services.AddTransient<LoginUserInteractor>();
            services.AddTransient<RegisterUserInteractor>();
            services.AddTransient<UpdateUserInteractor>();
            services.AddTransient<UserPasswordUpdater>();

            services.AddTransient<AbstractValidator<RegisterUserRequest>, RegisterUserRequestValidator>();
            services.AddTransient<AbstractValidator<UpdateUserRequest>, UserUpdateRequestValidator>();
            services.AddTransient<AbstractValidator<UserPasswordUpdateRequest>, UserPasswordUpdateValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
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
