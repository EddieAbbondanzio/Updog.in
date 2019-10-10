using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using Microsoft.AspNetCore.Authorization;
using Updog.Domain.Paging;
using System.Collections.Generic;

namespace Updog.Api {
    /// <summary>
    /// End point for managing sub spaces of the site.
    /// </summary>
    [Authorize]
    [Route("api/space")]
    [ApiController]
    public sealed class SpaceController : ApiController {
        #region Fields
        private QueryHandler<SpaceFindQuery, PagedResultSet<SpaceReadView>> spaceFinder;
        private QueryHandler<SpaceFindByNameQuery, SpaceReadView?> spaceFinderByName;
        private QueryHandler<SubscribedSpaceQuery, IEnumerable<SpaceReadView>> subsriptionFinderByUser;
        private QueryHandler<DefaultSpaceQuery, IEnumerable<SpaceReadView>> spaceFinderDefault;
        private CommandHandler<SpaceCreateCommand> spaceCreator;
        private CommandHandler<SpaceUpdateCommand> spaceUpdater;
        private QueryHandler<PostFindBySpaceQuery, PagedResultSet<PostReadView>> postFinderBySpace;
        #endregion

        #region Constructor(s)
        public SpaceController(
            QueryHandler<SpaceFindQuery, PagedResultSet<SpaceReadView>> spaceFinder,
            QueryHandler<SpaceFindByNameQuery, SpaceReadView?> spaceFinderByName,
            QueryHandler<SubscribedSpaceQuery, IEnumerable<SpaceReadView>> subscriptionFinder,
            QueryHandler<DefaultSpaceQuery, IEnumerable<SpaceReadView>> spaceFinderDefault,
            CommandHandler<SpaceCreateCommand> spaceCreator,
            CommandHandler<SpaceUpdateCommand> spaceUpdater,
            QueryHandler<PostFindBySpaceQuery, PagedResultSet<PostReadView>> postFinderBySpace
        ) {
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
        public async Task<IActionResult> GetDefaultSpaces() {
            var spaces = await spaceFinderDefault.Execute(new DefaultSpaceQuery());
            return Ok(spaces);
        }

        /// <summary>
        /// Get the subscribed spaces of the user.
        /// </summary>
        [HttpGet("subscribed")]
        public async Task<IActionResult> GetSubscribedSpaces() {
            var spaces = await subsriptionFinderByUser.Execute(new SubscribedSpaceQuery() {
                User = User!
            });

            return Ok(spaces);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) {
            var spaces = await this.spaceFinder.Execute(new SpaceFindQuery() {
                Paging = new PaginationInfo(pageNumber, pageSize)
            });

            SetContentRangeHeader(spaces.Pagination);
            return Ok(spaces);
        }


        /// <summary>
        /// Get a space via it's name.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        [HttpHead("{name}")]
        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindByName(string name) {
            var space = await spaceFinderByName.Execute(new SpaceFindByNameQuery() {
                Name = name
            });

            return space != null ? Ok(space) : NotFound() as IActionResult;
        }

        /// <summary>
        /// Create a new sub space.
        /// </summary>
        /// <param name="request">The incoming reuqest</param>
        [HttpPost]
        public async Task<IActionResult> CreateSpace(SpaceCreateRequest request) {
            var result = await spaceCreator.Execute(new SpaceCreateCommand(new SpaceCreate(request.Name, request.Description), User!));
            return result.IsSuccess ? Ok(result.InsertId) : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<IActionResult> UpdateSpace(string name, SpaceUpdateRequest request) {
            var result = await spaceUpdater.Execute(new SpaceUpdateCommand(name, new SpaceUpdate(request.Description), User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
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
        public async Task<IActionResult> FindPosts(string name, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            var posts = await this.postFinderBySpace.Execute(new PostFindBySpaceQuery() {
                Space = name,
                User = User!,
                Paging = new PaginationInfo(pageNumber, pageSize)
            });

            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }
        #endregion
    }
}