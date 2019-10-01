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
                    ValidateIssuer = false,
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
                        IDatabase db = c.HttpContext.RequestServices.GetService<IDatabase>();

                        using (var context = db.GetContext()) {
                            IUserRepo userRepo = context.GetRepo<IUserRepo>();

                            IIdentity identity = c.Principal.Identity;
                            User? u = await userRepo.FindById(userId);

                            if (u != null) {
                                u.AddIdentity(identity as ClaimsIdentity);

                                /*
                                 * Don't attempt to set this via c.HttpContext.Principal, ASP.NET seems to overwrite this later on...
                                 */
                                c.Principal = u;
                            }
                        }
                    }
                };
            });

            IDatabase db = new PostgresDatabase(Configuration.GetSection("Database").Get<DatabaseConfig>());
            db.RegisterRepo<IUserRepo, UserRepo>();
            db.RegisterRepo<IPostRepo, PostRepo>();
            db.RegisterRepo<ICommentRepo, CommentRepo>();
            db.RegisterRepo<ISpaceRepo, SpaceRepo>();
            db.RegisterRepo<ISubscriptionRepo, SubscriptionRepo>();
            db.RegisterRepo<IVoteRepo, VoteRepo>();

            services.AddSingleton<IDatabase>(db);
            services.AddSingleton<IEventBus, EventBus>();

            services.ConfigurePoco<IDatabaseConfig, DatabaseConfig>(Configuration.GetSection("Database"));
            services.ConfigurePoco<IAuthenticationTokenConfig, AuthenticationTokenConfig>(Configuration.GetSection("AuthenticationToken"));
            services.ConfigurePoco<IAdminConfig, AdminConfig>(Configuration.GetSection("Admin"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IAuthenticationTokenHandler, JsonWebTokenHandler>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddSingleton<IUserViewMapper, UserViewMapper>();
            services.AddSingleton<IUserRecordMapper, UserRecordMapper>();
            services.AddTransient<UserFinderByUsername>();
            services.AddTransient<UserLoginInteractor>();
            services.AddTransient<UserRegistrar>();
            services.AddTransient<UserUpdater>();
            services.AddTransient<UserPasswordUpdater>();
            services.AddTransient<AdminRegistrar>();

            services.AddSingleton<IPostViewMapper, PostViewMapper>();
            services.AddSingleton<PermissionHandler<Post>, PostPermissionHandler>();
            services.AddSingleton<IPostRecordMapper, PostRecordMapper>();
            services.AddTransient<PostCreator>();
            services.AddTransient<PostFinderById>();
            services.AddTransient<PostFinderBySpace>();
            services.AddTransient<PostFinderByUser>();
            services.AddTransient<PostFinderByNew>();
            services.AddTransient<PostDeleter>();
            services.AddTransient<PostUpdater>();

            services.AddSingleton<PermissionHandler<Comment>, CommentPermissionHandler>();
            services.AddSingleton<ICommentViewMapper, CommentViewMapper>();
            services.AddSingleton<ICommentRecordMapper, CommentRecordMapper>();
            services.AddTransient<CommentCreator>();
            services.AddTransient<CommentFinderById>();
            services.AddTransient<CommentFinderByPost>();
            services.AddTransient<CommentFinderByUser>();
            services.AddTransient<CommentDeleter>();
            services.AddTransient<CommentUpdater>();

            services.AddSingleton<PermissionHandler<Space>, SpacePermissionHandler>();
            services.AddSingleton<ISpaceViewMapper, SpaceViewMapper>();
            services.AddTransient<ISpaceRecordMapper, SpaceRecordMapper>();
            services.AddTransient<SpaceFinderDefault>();
            services.AddTransient<SpaceFinderByName>();
            services.AddTransient<SpaceFinder>();
            services.AddTransient<SpaceCreator>();
            services.AddTransient<SpaceUpdater>();

            services.AddSingleton<ISubscriptionViewMapper, SubscriptionViewMapper>();
            services.AddSingleton<ISubscriptionRecordMapper, SubscriptionRecordMapper>();
            services.AddTransient<SubscriptionFinderByUser>();
            services.AddTransient<SubscriptionCreator>();
            services.AddTransient<SubscriptionDeleter>();

            services.AddSingleton<IVoteViewMapper, VoteViewMapper>();
            services.AddTransient<PostVoter>();
            services.AddTransient<CommentVoter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors(b => {
                b.AllowAnyOrigin();
                b.AllowAnyMethod();
                b.AllowAnyHeader();
                b.WithExposedHeaders("Content-Range");
            });

            app.UseAuthentication();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseMiddleware<ExceptionHandler>();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
