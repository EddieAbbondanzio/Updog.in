using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using Microsoft.AspNetCore.Authorization;
using Updog.Domain.Paging;
using System.Collections.Generic;
using System;

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
        public async Task<IActionResult> GetDefaultSpaces() =>
            (await mediator.Query<DefaultSpaceQuery, IEnumerable<SpaceReadView>>(new DefaultSpaceQuery(User)))
            .Match<IActionResult>(
                spaces => Ok(spaces),
                error => BadRequest(error.Message)
            );

        /// <summary>
        /// Get the subscribed spaces of the user.
        /// </summary>
        [HttpGet("subscribed")]
        public async Task<IActionResult> GetSubscribedSpaces() =>
            (await mediator.Query<SubscribedSpaceQuery, IEnumerable<SpaceReadView>>(new SubscribedSpaceQuery(User!)))
            .Match<IActionResult>(
                spaces => Ok(spaces),
                error => BadRequest(error.Message)
            );

        [HttpGet]
        [ContentRangeHeader]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) =>
        (await this.mediator.Query<SpaceFindQuery, PagedResultSet<SpaceReadView>>(new SpaceFindQuery(new PaginationInfo(pageNumber, pageSize), User)))
        .Match<IActionResult>(
            spaces => Ok(spaces),
            e => BadRequest(e.Message)
        );

        /// <summary>
        /// Get a space via it's name.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        [HttpHead("{name}")]
        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindByName(string name) =>
            (await mediator.Query<SpaceFindByNameQuery, SpaceReadView?>(new SpaceFindByNameQuery(name, User)))
            .Match<IActionResult>(
                s => s != null ? Ok(s) : NotFound() as IActionResult,
                e => BadRequest(e.Message)
            );

        /// <summary>
        /// Create a new sub space.
        /// </summary>
        /// <param name="request">The incoming reuqest</param>
        [HttpPost]
        public async Task<IActionResult> CreateSpace(SpaceCreateRequest request) =>
            (await mediator.Command(new SpaceCreateCommand(new SpaceCreate(request.Name, request.Description), User!)))
            .Match<IActionResult>(
                r => Ok(new { Id = r.InsertId }),
                e => BadRequest(e.Message)
            );

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<IActionResult> UpdateSpace(string name, SpaceUpdateRequest request) =>
            (await mediator.Command(new SpaceUpdateCommand(name, new SpaceUpdate(request.Description), User!)))
            .Match<IActionResult>(
                r => Ok(),
                e => BadRequest(e.Message)
            );

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
        public async Task<IActionResult> FindPosts(string name, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) =>
            (await this.mediator.Query<PostFindBySpaceQuery, PagedResultSet<PostReadView>>(new PostFindBySpaceQuery(name, new PaginationInfo(pageNumber, pageSize), User)))
            .Match<IActionResult>(
                posts => Ok(posts),
                error => BadRequest(error.Message)
            );

        [AllowAnonymous]
        [HttpGet("moderator")]
        public async Task<IActionResult> GetModerators(string space) =>
            (await mediator.Query<FindModeratorsBySpaceQuery, IEnumerable<UserReadView>>(new FindModeratorsBySpaceQuery(space, User)))
            .Match<IActionResult>(
                mods => Ok(mods),
                error => BadRequest(error.Message)
            );

        [HttpPost("moderator")]
        public async Task<IActionResult> AddModerator(string space, string username) =>
            (await mediator.Command<AddModeratorToSpaceCommand>(new AddModeratorToSpaceCommand(space, username, User!)))
            .Match<IActionResult>(
                r => Ok(),
                e => BadRequest(e.Message)
            );

        [HttpPost("moderator")]
        public async Task<IActionResult> RemoveModerator(string space, string username) =>
            (await mediator.Command(new RemoveModeratorFromSpaceCommand(space, username, User!)))
            .Match<IActionResult>(
                r => Ok(),
                e => BadRequest(e.Message)
            );
        #endregion
    }
}