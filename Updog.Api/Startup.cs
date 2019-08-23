using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Infrastructure;
using Updog.Persistance;
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
using Updog.Domain;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Updog.Api {
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
                    RequireSignedTokens = true,
                    ValidIssuer = Configuration["AuthenticationToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthenticationToken:Secret"]))

                };

                opts.Events = new JwtBearerEvents() {
                    OnTokenValidated = async (c) => {
                        // Figure out the user ID the token belongs to.
                        Claim subjectClaim = c.Principal.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
                        int userId = Convert.ToInt32(subjectClaim.Value);

                        // Retrieve the user, and cache the identity.
                        IUserRepo userRepo = c.HttpContext.RequestServices.GetService<IUserRepo>();
                        IIdentity identity = c.Principal.Identity;

                        User u = await userRepo.FindById(userId);
                        u.AddIdentity(identity as ClaimsIdentity);

                        /*
                         * Don't attempt to set this via c.HttpContext.Principal, ASP.NET seems to overwrite this later on...
                         */
                        c.Principal = u;
                    }
                };
            });


            services.ConfigurePoco<IDatabaseConfig, DatabaseConfig>(Configuration.GetSection("Database"));
            services.ConfigurePoco<IAuthenticationTokenConfig, AuthenticationTokenConfig>(Configuration.GetSection("AuthenticationToken"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IAuthenticationTokenHandler, JsonWebTokenHandler>();
            services.AddSingleton<IDatabase, MySqlDatabase>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IPostRepo, PostRepo>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddSingleton<IMapper<User, UserView>, UserViewMapper>();
            services.AddSingleton<IUserRecordMapper, UserRecordMapper>();
            services.AddTransient<UserFinderByUsername>();
            services.AddTransient<UserLoginInteractor>();
            services.AddTransient<UserRegisterInteractor>();
            services.AddTransient<UpdateUserInteractor>();
            services.AddTransient<UserPasswordUpdater>();

            services.AddTransient<AbstractValidator<UserRegisterParams>, UserRegisterValidator>();
            services.AddTransient<AbstractValidator<UpdateUserParams>, UserUpdateValidator>();
            services.AddTransient<AbstractValidator<UserPasswordUpdateParams>, UserPasswordValidator>();

            services.AddSingleton<IMapper<Post, PostView>, PostViewMapper>();
            services.AddSingleton<IPermissionHandler<Post>, PostPermissionHandler>();
            services.AddSingleton<IPostRecordMapper, PostRecordMapper>();
            services.AddTransient<IPostRepo, PostRepo>();
            services.AddTransient<PostCreator>();
            services.AddTransient<PostFinderById>();
            services.AddTransient<PostFinderByNew>();
            services.AddTransient<PostFinderByUser>();
            services.AddTransient<PostDeleter>();
            services.AddTransient<PostUpdater>();

            services.AddTransient<AbstractValidator<PostCreateParams>, PostCreateValidator>();
            services.AddTransient<AbstractValidator<PostUpdateParams>, PostUpdateValidator>();
            services.AddTransient<AbstractValidator<PostDeleteParams>, PostDeleteValidator>();

            services.AddSingleton<IPermissionHandler<Comment>, CommentPermissionHandler>();
            services.AddSingleton<IMapper<Comment, CommentView>, CommentViewMapper>();
            services.AddSingleton<ICommentRecordMapper, CommentRecordMapper>();
            services.AddTransient<ICommentRepo, CommentRepo>();
            services.AddTransient<CommentCreator>();
            services.AddTransient<CommentFinderById>();
            services.AddTransient<CommentFinderByPost>();
            services.AddTransient<CommentFinderByUser>();
            services.AddTransient<CommentDeleter>();
            services.AddTransient<CommentUpdater>();
            services.AddTransient<AbstractValidator<CommentCreateParams>, CommentCreateValidator>();
            services.AddTransient<AbstractValidator<CommentUpdateParams>, CommentUpdateValidator>();
            services.AddTransient<AbstractValidator<CommentDeleteParams>, CommentDeleteValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors(b => {
                b.AllowAnyOrigin();
                b.AllowAnyMethod();
                b.AllowAnyHeader();
            });

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
