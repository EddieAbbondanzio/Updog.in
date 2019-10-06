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
            services.AddCors();

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IDatabase, PostgresDatabase>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IPostRepo, PostRepo>();
            services.AddTransient<ICommentRepo, CommentRepo>();
            services.AddTransient<ISpaceRepo, SpaceRepo>();
            services.AddTransient<ISubscriptionRepo, SubscriptionRepo>();
            services.AddTransient<IVoteRepo, VoteRepo>();

            services.AddSingleton<IEventBus, EventBus>();

            services.ConfigurePoco<IDatabaseConfig, DatabaseConfig>(Configuration.GetSection("Database"));
            services.ConfigurePoco<IAuthenticationTokenConfig, AuthenticationTokenConfig>(Configuration.GetSection("AuthenticationToken"));
            services.ConfigurePoco<IAdminConfig, AdminConfig>(Configuration.GetSection("Admin"));


            services.AddSingleton<IAuthenticationTokenHandler, JsonWebTokenHandler>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IUserViewMapper, UserViewMapper>();
            services.AddSingleton<IUserRecordMapper, UserRecordMapper>();
            services.AddTransient<QueryHandler<FindUserByUsernameQuery>, FindUserByUsernameQueryHandler>();
            services.AddTransient<QueryHandler<IsUsernameAvailableQuery>, IsUsernameAvailableQueryHandler>();
            services.AddTransient<CommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();
            services.AddTransient<CommandHandler<LoginUserCommand>, LoginUserCommandHandler>();
            services.AddTransient<CommandHandler<UserUpdateCommand>, UserUpdateCommandHandler>();
            services.AddTransient<CommandHandler<UserUpdatePasswordCommand>, UserUpdatePasswordCommandHandler>();
            services.AddTransient<CommandHandler<AdminRegisterOrUpdateCommand>, AdminRegisterOrUpdateCommandHandler>();

            services.AddSingleton<IPostViewMapper, PostViewMapper>();
            services.AddSingleton<IPostFactory, PostFactory>();
            services.AddSingleton<PermissionHandler<Post>, PostPermissionHandler>();
            services.AddSingleton<IPostRecordMapper, PostRecordMapper>();
            services.AddTransient<CommandHandler<PostCreateCommand>, PostCreateCommandHandler>();
            services.AddTransient<CommandHandler<PostUpdateCommand>, PostUpdateCommandHandler>();
            services.AddTransient<CommandHandler<PostDeleteCommand>, PostDeleteCommandHandler>();
            services.AddTransient<QueryHandler<PostFindByIdQuery>, PostFindByIdQueryHandler>();
            services.AddTransient<QueryHandler<PostFindBySpaceQuery>, PostFindBySpaceQueryHandler>();
            services.AddTransient<QueryHandler<PostFindByUserQuery>, PostFindByUserQueryHandler>();
            services.AddTransient<QueryHandler<PostFindByNewQuery>, PostFindByNewQueryHandler>();

            services.AddSingleton<PermissionHandler<Comment>, CommentPermissionHandler>();
            services.AddSingleton<ICommentFactory, CommentFactory>();
            services.AddSingleton<ICommentViewMapper, CommentViewMapper>();
            services.AddSingleton<ICommentRecordMapper, CommentRecordMapper>();
            services.AddTransient<QueryHandler<CommentFindByIdQuery>, CommentFindByIdQueryHandler>();
            services.AddTransient<QueryHandler<CommentFindByPostQuery>, CommentFindByPostQueryHandler>();
            services.AddTransient<QueryHandler<CommentFindByUserQuery>, CommentFindByUserQueryHandler>();
            services.AddTransient<CommandHandler<CommentCreateCommand>, CommentCreateCommandHandler>();
            services.AddTransient<CommandHandler<CommentUpdateCommand>, CommentUpdateCommandHandler>();
            services.AddTransient<CommandHandler<CommentDeleteCommand>, CommentDeleteCommandHandler>();

            services.AddSingleton<PermissionHandler<Space>, SpacePermissionHandler>();
            services.AddSingleton<ISpaceViewMapper, SpaceViewMapper>();
            services.AddSingleton<ISpaceFactory, SpaceFactory>();
            services.AddTransient<ISpaceRecordMapper, SpaceRecordMapper>();
            services.AddTransient<QueryHandler<DefaultSpaceQuery>, DefaultSpaceQueryHandler>();
            services.AddTransient<QueryHandler<SpaceFindByNameQuery>, SpaceFindByNameQueryHandler>();
            services.AddTransient<QueryHandler<SpaceFindQuery>, SpaceFindQueryHandler>();
            services.AddTransient<CommandHandler<SpaceCreateCommand>, SpaceCreateCommandHandler>();
            services.AddTransient<CommandHandler<SpaceUpdateCommand>, SpaceUpdateCommandHandler>();

            services.AddSingleton<ISubscriptionFactory, SubscriptionFactory>();
            services.AddSingleton<ISubscriptionRecordMapper, SubscriptionRecordMapper>();
            services.AddTransient<QueryHandler<SubscribedSpaceQuery>, SubscribedSpaceQueryHandler>();
            services.AddTransient<CommandHandler<SubscriptionCreateCommand>, SubscriptionCreateCommandHandler>();
            services.AddTransient<CommandHandler<SubscriptionDeleteCommand>, SubscriptionDeleteCommandHandler>();

            services.AddSingleton<IVoteViewMapper, VoteViewMapper>();
            services.AddSingleton<IVoteFactory, VoteFactory>();
            services.AddTransient<CommandHandler<VoteOnPostCommand>, VoteOnPostCommandHandler>();
            services.AddTransient<CommandHandler<VoteOnCommentCommand>, VoteOnCommentCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            Console.WriteLine($"Hosting environment: {env.EnvironmentName}");
            Console.WriteLine($"Content root path: {env.ContentRootPath}");

            app.UseCors(b => {
                // b.WithOrigins("https://localhost:8080");
                b.AllowAnyOrigin();
                b.AllowAnyMethod();
                b.AllowAnyHeader();
                b.WithExposedHeaders("Content-Range");
            });

            app.UseAuthentication();

            if (env.IsDevelopment()) {
                app.UseHttpsRedirection();
                app.UseDeveloperExceptionPage();
            } else {
                app.UseMiddleware<ExceptionHandler>();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }


            app.UseMvc();

            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json");
        }
    }
}
