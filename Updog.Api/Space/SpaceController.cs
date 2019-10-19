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
            .Match(
                spaces => Ok(spaces) as IActionResult,
                error => BadRequest(error.Message)
            );

        /// <summary>
        /// Get the subscribed spaces of the user.
        /// </summary>
        [HttpGet("subscribed")]
        public async Task<IActionResult> GetSubscribedSpaces() =>
            (await mediator.Query<SubscribedSpaceQuery, IEnumerable<SpaceReadView>>(new SubscribedSpaceQuery(User!)))
            .Match(
                spaces => Ok(spaces) as IActionResult,
                error => BadRequest(error.Message)
            );

        [HttpGet]
        [ContentRangeHeader]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) =>
        (await this.mediator.Query<SpaceFindQuery, PagedResultSet<SpaceReadView>>(new SpaceFindQuery(new PaginationInfo(pageNumber, pageSize), User)))
        .Match(
            spaces => Ok(spaces) as IActionResult,
            e => BadRequest(e.Message) as IActionResult
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
            .Match(
                s => s != null ? Ok(s) : NotFound() as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );

        /// <summary>
        /// Create a new sub space.
        /// </summary>
        /// <param name="request">The incoming reuqest</param>
        [HttpPost]
        public async Task<IActionResult> CreateSpace(SpaceCreateRequest request) =>
            (await mediator.Command(new SpaceCreateCommand(new SpaceCreate(request.Name, request.Description), User!)))
            .Match(
                r => Ok(new { Id = r.InsertId }) as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<IActionResult> UpdateSpace(string name, SpaceUpdateRequest request) =>
            (await mediator.Command(new SpaceUpdateCommand(name, new SpaceUpdate(request.Description), User!)))
            .Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message) as IActionResult
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
            .Match(
                posts => Ok(posts) as IActionResult,
                error => BadRequest(error.Message) as IActionResult
            );

        [AllowAnonymous]
        [HttpGet("moderator")]
        public async Task<IActionResult> GetModerators(string space) =>
            (await mediator.Query<FindModeratorsBySpaceQuery, IEnumerable<UserReadView>>(new FindModeratorsBySpaceQuery(space, User)))
            .Match(
                mods => Ok(mods) as IActionResult,
                error => BadRequest(error.Message)
            );

        [HttpPost("moderator")]
        public async Task<IActionResult> AddModerator(string space, string username) =>
            (await mediator.Command<AddModeratorToSpaceCommand>(new AddModeratorToSpaceCommand(space, username, User!)))
            .Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );

        [HttpPost("moderator")]
        public async Task<IActionResult> RemoveModerator(string space, string username) =>
            (await mediator.Command(new RemoveModeratorFromSpaceCommand(space, username, User!)))
            .Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );
        #endregion
    }
}