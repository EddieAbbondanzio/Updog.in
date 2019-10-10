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
using Updog.Domain.Paging;

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

            services.AddScoped<IEventBus, EventBus>();

            services.ConfigurePoco<IDatabaseConfig, DatabaseConfig>(Configuration.GetSection("Database"));
            services.ConfigurePoco<IAuthenticationTokenConfig, AuthenticationTokenConfig>(Configuration.GetSection("AuthenticationToken"));
            services.ConfigurePoco<IAdminConfig, AdminConfig>(Configuration.GetSection("Admin"));


            services.AddSingleton<IAuthenticationTokenHandler, JsonWebTokenHandler>();

            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IUserReader, UserReader>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IUserMapper, UserMapper>();
            services.AddSingleton<IUserReadViewMapper, UserReadViewMapper>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddTransient<QueryHandler<FindUserByUsernameQuery, UserReadView?>, FindUserByUsernameQueryHandler>();
            services.AddTransient<QueryHandler<IsUsernameAvailableQuery, bool>, IsUsernameAvailableQueryHandler>();
            services.AddTransient<CommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();
            services.AddTransient<CommandHandler<LoginUserCommand>, LoginUserCommandHandler>();
            services.AddTransient<CommandHandler<UserUpdateCommand>, UserUpdateCommandHandler>();
            services.AddTransient<CommandHandler<UserUpdatePasswordCommand>, UserUpdatePasswordCommandHandler>();
            services.AddTransient<CommandHandler<AdminRegisterOrUpdateCommand>, AdminRegisterOrUpdateCommandHandler>();

            services.AddTransient<IPostRepo, PostRepo>();
            services.AddTransient<IPostReader, PostReader>();
            services.AddTransient<IPostService, PostService>();
            services.AddSingleton<IPostMapper, PostMapper>();
            services.AddSingleton<IPostReadViewMapper, PostReadViewMapper>();
            services.AddSingleton<IPostFactory, PostFactory>();
            services.AddTransient<IDomainEventHandler<VoteOnPostEvent>, VoteOnPostEventHandler>();
            services.AddTransient<QueryHandler<PostFindByIdQuery, PostReadView?>, PostFindByIdQueryHandler>();
            services.AddTransient<QueryHandler<PostFindBySpaceQuery, PagedResultSet<PostReadView>>, PostFindBySpaceQueryHandler>();
            services.AddTransient<QueryHandler<PostFindByUserQuery, PagedResultSet<PostReadView>>, PostFindByUserQueryHandler>();
            services.AddTransient<QueryHandler<PostFindByNewQuery, PagedResultSet<PostReadView>>, PostFindByNewQueryHandler>();
            services.AddTransient<CommandHandler<PostCreateCommand>, PostCreateCommandHandler>();
            services.AddTransient<CommandHandler<PostUpdateCommand>, PostUpdateCommandHandler>();
            services.AddTransient<CommandHandler<PostDeleteCommand>, PostDeleteCommandHandler>();


            services.AddTransient<ICommentRepo, CommentRepo>();
            services.AddTransient<ICommentReader, CommentReader>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddSingleton<ICommentMapper, CommentMapper>();
            services.AddSingleton<ICommentReadViewMapper, CommentReadViewMapper>();
            services.AddSingleton<ICommentFactory, CommentFactory>();
            services.AddTransient<IDomainEventHandler<VoteOnCommentEvent>, VoteOnCommentEventHandler>();
            services.AddTransient<QueryHandler<CommentFindByIdQuery, CommentReadView?>, CommentFindByIdQueryHandler>();
            services.AddTransient<QueryHandler<CommentFindByPostQuery, IEnumerable<CommentReadView>>, CommentFindByPostQueryHandler>();
            services.AddTransient<QueryHandler<CommentFindByUserQuery, PagedResultSet<CommentReadView>>, CommentFindByUserQueryHandler>();
            services.AddTransient<CommandHandler<CommentCreateCommand>, CommentCreateCommandHandler>();
            services.AddTransient<CommandHandler<CommentUpdateCommand>, CommentUpdateCommandHandler>();
            services.AddTransient<CommandHandler<CommentDeleteCommand>, CommentDeleteCommandHandler>();

            services.AddTransient<ISpaceRepo, SpaceRepo>();
            services.AddTransient<ISpaceReader, SpaceReader>();
            services.AddTransient<ISpaceService, SpaceService>();
            services.AddSingleton<ISpaceMapper, SpaceMapper>();
            services.AddSingleton<ISpaceReadViewMapper, SpaceReadViewMapper>();
            services.AddSingleton<ISpaceFactory, SpaceFactory>();
            services.AddTransient<IDomainEventHandler<SubscriptionCreateEvent>, SubscriptionCreateEventHandler>();
            services.AddTransient<IDomainEventHandler<SubscriptionDeleteEvent>, SubscriptionDeleteEventHandler>();
            services.AddTransient<QueryHandler<DefaultSpaceQuery, IEnumerable<SpaceReadView>>, DefaultSpaceQueryHandler>();
            services.AddTransient<QueryHandler<SubscribedSpaceQuery, IEnumerable<SpaceReadView>>, SubscribedSpaceQueryHandler>();
            services.AddTransient<QueryHandler<SpaceFindByNameQuery, SpaceReadView?>, SpaceFindByNameQueryHandler>();
            services.AddTransient<QueryHandler<SpaceFindQuery, PagedResultSet<SpaceReadView>>, SpaceFindQueryHandler>();
            services.AddTransient<CommandHandler<SpaceCreateCommand>, SpaceCreateCommandHandler>();
            services.AddTransient<CommandHandler<SpaceUpdateCommand>, SpaceUpdateCommandHandler>();

            services.AddTransient<ISubscriptionRepo, SubscriptionRepo>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddSingleton<ISubscriptionMapper, SubscriptionMapper>();
            services.AddSingleton<ISubscriptionFactory, SubscriptionFactory>();
            services.AddTransient<CommandHandler<SubscriptionCreateCommand>, SubscriptionCreateCommandHandler>();
            services.AddTransient<CommandHandler<SubscriptionDeleteCommand>, SubscriptionDeleteCommandHandler>();

            services.AddTransient<IVoteRepo, VoteRepo>();
            services.AddTransient<IVoteReader, VoteReader>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddSingleton<IVoteReadViewMapper, VoteReadViewMapper>();
            services.AddSingleton<IVoteMapper, VoteMapper>();
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
