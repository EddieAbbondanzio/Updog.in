using System;
using System.Collections.Generic;
using System.Linq;
using Updog.Application;
using Updog.Infrastructure;
using Updog.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dapper;
using System.Text;
using System.Security.Claims;
using Updog.Domain;
using System.Security.Principal;
using Updog.Domain.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Updog.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
            SqlMapper.AddTypeHandler(new DateTimeHandler());
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors();

            // Set up the authentication
            services.AddAuthentication("Bearer").AddJwtBearer(opts => {
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
                    // When a JWT is validated, go out to the DB and pull in the relevant user.
                    OnTokenValidated = async (c) => {
                        IDatabase db = c.HttpContext.RequestServices.GetService<IDatabase>();

                        using (var databaseContext = db.GetContext()) {
                            IUserRepo userRepo = databaseContext.GetRepo<IUserRepo>();
                            User? user = await userRepo.FindById(c.HttpContext.User.GetUserId());

                            c.HttpContext.SetActiveUser(user);
                        }
                    }
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);



            var dbConfig = services.ConfigurePoco<IDatabaseConfig, PostgresDatabaseConfig>(Configuration.GetSection("Database"));
            services.ConfigurePoco<IAuthenticationTokenConfig, AuthenticationTokenConfig>(Configuration.GetSection("AuthenticationToken"));
            services.ConfigurePoco<IAdminConfig, AdminConfig>(Configuration.GetSection("Admin"));

            services.AddSingleton<IDatabase, PostgresDatabase>();
            services.AddDatabaseMigrations(dbConfig.GetConnectionString());
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IEventBus, EventBus>();
            services.AddSingleton<IAuthenticationTokenHandler, JsonWebTokenHandler>();
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IUserReader, UserReader>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddScoped<QueryHandler<FindUserByUsernameQuery, UserReadView?>, FindUserByUsernameQueryHandler>();
            services.AddScoped<QueryHandler<IsUsernameAvailableQuery, bool>, IsUsernameAvailableQueryHandler>();
            services.AddScoped<CommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();
            services.AddScoped<CommandHandler<LoginUserCommand>, LoginUserCommandHandler>();
            services.AddScoped<CommandHandler<UserUpdateCommand>, UserUpdateCommandHandler>();
            services.AddScoped<CommandHandler<UserUpdatePasswordCommand>, UserUpdatePasswordCommandHandler>();
            services.AddScoped<CommandHandler<AdminRegisterOrUpdateCommand>, AdminRegisterOrUpdateCommandHandler>();
            services.AddTransient<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddTransient<IValidator<LoginUserCommand>, LoginUserCommandValidator>();
            services.AddTransient<IValidator<UserUpdateCommand>, UserUpdateCommandValidator>();
            services.AddTransient<IValidator<UserUpdatePasswordCommand>, UserUpdatePasswordCommandValidator>();

            services.AddTransient<IPostRepo, PostRepo>();
            services.AddTransient<IPostReader, PostReader>();
            services.AddTransient<IPostService, PostService>();
            services.AddSingleton<IPostFactory, PostFactory>();
            services.AddTransient<IDomainEventHandler<VoteOnPostEvent>, VoteOnPostEventHandler>();
            services.AddScoped<QueryHandler<PostFindByIdQuery, PostReadView?>, PostFindByIdQueryHandler>();
            services.AddScoped<QueryHandler<PostFindBySpaceQuery, PagedResultSet<PostReadView>>, PostFindBySpaceQueryHandler>();
            services.AddScoped<QueryHandler<PostFindByUserQuery, PagedResultSet<PostReadView>>, PostFindByUserQueryHandler>();
            services.AddScoped<QueryHandler<PostFindByNewQuery, PagedResultSet<PostReadView>>, PostFindByNewQueryHandler>();
            services.AddScoped<CommandHandler<PostCreateCommand>, PostCreateCommandHandler>();
            services.AddScoped<CommandHandler<PostUpdateCommand>, PostUpdateCommandHandler>();
            services.AddScoped<CommandHandler<PostDeleteCommand>, PostDeleteCommandHandler>();
            services.AddTransient<IPolicy<PostAlterCommand>, PostAlterCommandPolicy>();
            services.AddTransient<IValidator<PostCreateCommand>, PostCreateCommandValidator>();
            services.AddTransient<IValidator<PostUpdateCommand>, PostUpdateCommandValidator>();
            services.AddTransient<IValidator<PostDeleteCommand>, PostDeleteCommandValidator>();

            services.AddTransient<ICommentRepo, CommentRepo>();
            services.AddTransient<ICommentReader, CommentReader>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddSingleton<ICommentFactory, CommentFactory>();
            services.AddTransient<IDomainEventHandler<VoteOnCommentEvent>, VoteOnCommentEventHandler>();
            services.AddScoped<QueryHandler<CommentFindByIdQuery, CommentReadView?>, CommentFindByIdQueryHandler>();
            services.AddScoped<QueryHandler<CommentFindByPostQuery, IEnumerable<CommentReadView>>, CommentFindByPostQueryHandler>();
            services.AddScoped<QueryHandler<CommentFindByUserQuery, PagedResultSet<CommentReadView>>, CommentFindByUserQueryHandler>();
            services.AddScoped<CommandHandler<CommentCreateCommand>, CommentCreateCommandHandler>();
            services.AddScoped<CommandHandler<CommentUpdateCommand>, CommentUpdateCommandHandler>();
            services.AddScoped<CommandHandler<CommentDeleteCommand>, CommentDeleteCommandHandler>();
            services.AddTransient<IPolicy<CommentAlterCommand>, CommentAlterCommandPolicy>();
            services.AddTransient<IValidator<CommentCreateCommand>, CommentCreateCommandValidator>();
            services.AddTransient<IValidator<CommentUpdateCommand>, CommentUpdateCommandValidator>();
            services.AddTransient<IValidator<CommentDeleteCommand>, CommentDeleteCommandValidator>();

            services.AddTransient<ISpaceRepo, SpaceRepo>();
            services.AddTransient<ISpaceReader, SpaceReader>();
            services.AddTransient<ISpaceService, SpaceService>();
            services.AddSingleton<ISpaceFactory, SpaceFactory>();
            services.AddTransient<IDomainEventHandler<SubscriptionCreateEvent>, SubscriptionCreateEventHandler>();
            services.AddTransient<IDomainEventHandler<SubscriptionDeleteEvent>, SubscriptionDeleteEventHandler>();
            services.AddScoped<QueryHandler<DefaultSpaceQuery, IEnumerable<SpaceReadView>>, DefaultSpaceQueryHandler>();
            services.AddScoped<QueryHandler<SubscribedSpaceQuery, IEnumerable<SpaceReadView>>, SubscribedSpaceQueryHandler>();
            services.AddScoped<QueryHandler<SpaceFindByNameQuery, SpaceReadView?>, SpaceFindByNameQueryHandler>();
            services.AddScoped<QueryHandler<SpaceFindQuery, PagedResultSet<SpaceReadView>>, SpaceFindQueryHandler>();
            services.AddScoped<CommandHandler<SpaceCreateCommand>, SpaceCreateCommandHandler>();
            services.AddScoped<CommandHandler<SpaceUpdateCommand>, SpaceUpdateCommandHandler>();
            services.AddTransient<IPolicy<SpaceAlterCommand>, SpaceAlterCommandPolicy>();
            services.AddTransient<IValidator<SpaceCreateCommand>, SpaceCreateCommandValidator>();
            services.AddTransient<IValidator<SpaceUpdateCommand>, SpaceUpdateCommandValidator>();

            services.AddTransient<ISubscriptionRepo, SubscriptionRepo>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddSingleton<ISubscriptionFactory, SubscriptionFactory>();
            services.AddScoped<CommandHandler<SubscriptionCreateCommand>, SubscriptionCreateCommandHandler>();
            services.AddScoped<CommandHandler<SubscriptionDeleteCommand>, SubscriptionDeleteCommandHandler>();
            services.AddTransient<IValidator<SubscriptionCreateCommand>, SubscriptionCreateCommandValidator>();
            services.AddTransient<IValidator<SubscriptionDeleteCommand>, SubscriptionDeleteCommandValidator>();

            services.AddTransient<IVoteRepo, VoteRepo>();
            services.AddTransient<IVoteReader, VoteReader>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddSingleton<IVoteFactory, VoteFactory>();
            services.AddScoped<CommandHandler<VoteOnPostCommand>, VoteOnPostCommandHandler>();
            services.AddScoped<CommandHandler<VoteOnCommentCommand>, VoteOnCommentCommandHandler>();
            services.AddTransient<IValidator<VoteOnCommentCommand>, VoteOnCommentCommandValidator>();
            services.AddTransient<IValidator<VoteOnPostCommand>, VoteOnPostCommandValidator>();

            services.AddTransient<IRoleRepo, RoleRepo>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddSingleton<IRoleFactory, RoleFactory>();
            services.AddScoped<CommandHandler<AddAdminCommand>, AddAdminCommandHandler>();
            services.AddScoped<CommandHandler<AddModeratorToSpaceCommand>, AddModeratorToSpaceCommandHandler>();
            services.AddScoped<CommandHandler<RemoveAdminCommand>, RemoveAdminCommandHandler>();
            services.AddScoped<CommandHandler<RemoveModeratorFromSpaceCommand>, RemoveModeratorFromSpaceCommandHandler>();
            services.AddScoped<QueryHandler<FindAdminsQuery, IEnumerable<UserReadView>>, FindAdminsQueryHandler>();
            services.AddScoped<QueryHandler<FindModeratorsBySpaceQuery, IEnumerable<UserReadView>>, FindModeratorsBySpaceQueryHandler>();
            services.AddScoped<QueryHandler<FindSpacesUserModeratesQuery, IEnumerable<SpaceReadView>>, FindSpacesUserModeratesQueryHandler>();
            services.AddTransient<IPolicy<AddAdminCommand>, AddAdminCommandPolicy>();
            services.AddTransient<IPolicy<RemoveAdminCommand>, RemoveAdminCommandPolicy>();
            services.AddTransient<IPolicy<AddModeratorToSpaceCommand>, AddModeratorToSpaceCommandPolicy>();
            services.AddTransient<IPolicy<RemoveModeratorFromSpaceCommand>, RemoveModeratorFromSpaceCommandPolicy>();
            services.AddTransient<IValidator<AddAdminCommand>, AddAdminCommandValidator>();
            services.AddTransient<IValidator<RemoveAdminCommand>, RemoveAdminCommandValidator>();
            services.AddTransient<IValidator<AddModeratorToSpaceCommand>, AddModeratorToSpaceCommandValidator>();
            services.AddTransient<IValidator<RemoveModeratorFromSpaceCommand>, RemoveModeratorFromSpaceCommandValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            Console.WriteLine($"Hosting environment: {env.EnvironmentName}");
            Console.WriteLine($"Content root path: {env.ContentRootPath}");

            app.UseCors(b => {
                // b.WithOrigins("https://localhost:8080");
                b.AllowAnyOrigin();
                b.AllowAnyMethod();
                b.AllowAnyHeader();
                b.WithExposedHeaders("Content-Range");
            });

            app.UseRouting();
            app.UseAuthorization();

            if (env.IsDevelopment()) {
                app.UseHttpsRedirection();
                app.UseDeveloperExceptionPage();
            } else {
                app.UseMiddleware<ExceptionHandler>();
            }

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json");
        }
    }
}
