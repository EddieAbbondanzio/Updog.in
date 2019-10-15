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
        private IMediator mediator;
        #endregion

        #region Constructor(s)
        public SpaceController(IMediator mediator) {
            this.mediator = mediator;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get the default spaces.
        /// </summary>
        [HttpGet("default")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDefaultSpaces() {
            var spaces = await mediator.Query<DefaultSpaceQuery, IEnumerable<SpaceReadView>>(new DefaultSpaceQuery(User));
            return Ok(spaces);
        }

        /// <summary>
        /// Get the subscribed spaces of the user.
        /// </summary>
        [HttpGet("subscribed")]
        public async Task<IActionResult> GetSubscribedSpaces() {
            var spaces = await mediator.Query<SubscribedSpaceQuery, IEnumerable<SpaceReadView>>(new SubscribedSpaceQuery(User!));

            return Ok(spaces);
        }

        [HttpGet]
        [ContentRangeHeader]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) {
            var spaces = await this.mediator.Query<SpaceFindQuery, PagedResultSet<SpaceReadView>>(new SpaceFindQuery(new PaginationInfo(pageNumber, pageSize), User));

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
            var space = await mediator.Query<SpaceFindByNameQuery, SpaceReadView>(new SpaceFindByNameQuery(name, User));

            return space != null ? Ok(space) : NotFound() as IActionResult;
        }

        /// <summary>
        /// Create a new sub space.
        /// </summary>
        /// <param name="request">The incoming reuqest</param>
        [HttpPost]
        public async Task<IActionResult> CreateSpace(SpaceCreateRequest request) {
            var result = await mediator.Command(new SpaceCreateCommand(new SpaceCreate(request.Name, request.Description), User!));
            return result.IsSuccess ? Ok(result.InsertId) : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<IActionResult> UpdateSpace(string name, SpaceUpdateRequest request) {
            var result = await mediator.Command(new SpaceUpdateCommand(name, new SpaceUpdate(request.Description), User!));
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
        [ContentRangeHeader]
        [HttpGet("{name}/post/new")]
        public async Task<IActionResult> FindPosts(string name, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            var posts = await this.mediator.Query<PostFindBySpaceQuery, PagedResultSet<PostReadView>>(new PostFindBySpaceQuery(name, new PaginationInfo(pageNumber, pageSize), User));
            return Ok(posts);
        }
        #endregion
    }
}