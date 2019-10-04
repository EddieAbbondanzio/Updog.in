using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using Microsoft.AspNetCore.Authorization;
using Updog.Domain.Paging;

namespace Updog.Api {
    /// <summary>
    /// End point for managing sub spaces of the site.
    /// </summary>
    [Authorize]
    [Route("api/space")]
    [ApiController]
    public sealed class SpaceController : ApiController {
        #region Fields
        private QueryHandler<SpaceFindQuery> spaceFinder;
        private QueryHandler<SpaceFindByNameQuery> spaceFinderByName;
        private QueryHandler<SubscribedSpaceQuery> subsriptionFinderByUser;
        private QueryHandler<DefaultSpaceQuery> spaceFinderDefault;
        private CommandHandler<SpaceCreateCommand> spaceCreator;
        private CommandHandler<SpaceUpdateCommand> spaceUpdater;
        private QueryHandler<PostFindBySpaceQuery> postFinderBySpace;
        #endregion

        #region Constructor(s)
        public SpaceController(QueryHandler<SpaceFindQuery> spaceFinder, QueryHandler<SpaceFindByNameQuery> spaceFinderByName, QueryHandler<SubscribedSpaceQuery> subscriptionFinder, QueryHandler<DefaultSpaceQuery> spaceFinderDefault, CommandHandler<SpaceCreateCommand> spaceCreator, CommandHandler<SpaceUpdateCommand> spaceUpdater, QueryHandler<PostFindBySpaceQuery> postFinderBySpace) {
            this.spaceFinder = spaceFinder;
            this.spaceFinderByName = spaceFinderByName;
            this.subsriptionFinderByUser = subscriptionFinder;
            this.spaceFinderDefault = spaceFinderDefault;
            this.spaceCreator = spaceCreator;
            this.spaceUpdater = spaceUpdater;
            this.postFinderBySpace = postFinderBySpace;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get the default spaces.
        /// </summary>
        [HttpGet("default")]
        [AllowAnonymous]
        public async Task<ActionResult> GetDefaultSpaces() {
            await spaceFinderDefault.Execute(new DefaultSpaceQuery(), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Get the subscribed spaces of the user.
        /// </summary>
        [HttpGet("subscribed")]
        public async Task<ActionResult> GetSubscribedSpaces() {
            await subsriptionFinderByUser.Execute(new SubscribedSpaceQuery(User!.Username), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) {
            await this.spaceFinder.Execute(new SpaceFindQuery(new PaginationInfo(pageNumber, pageSize)), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }


        /// <summary>
        /// Get a space via it's name.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        [HttpHead("{name}")]
        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<ActionResult> FindByName(string name) {
            await spaceFinderByName.Execute(new SpaceFindByNameQuery(name), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Create a new sub space.
        /// </summary>
        /// <param name="request">The incoming reuqest</param>
        [HttpPost]
        public async Task<ActionResult> CreateSpace(SpaceCreateRequest request) {
            await spaceCreator.Execute(new SpaceCreateCommand(new SpaceCreationData(request.Name, request.Description), User!), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<ActionResult> UpdateSpace(string name, SpaceUpdateRequest request) {
            await spaceUpdater.Execute(new SpaceUpdateCommand(User!, name, request.Description), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Find the posts for a specific space.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        /// <param name="pageNumber">0-index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The posts found.</returns>
        [AllowAnonymous]
        [HttpGet("{name}/post/new")]
        public async Task<ActionResult> FindPosts(string name, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            await this.postFinderBySpace.Execute(new PostFindBySpaceQuery(name, User, new PaginationInfo(pageNumber, pageSize)), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }
        #endregion
    }
}