using Updog.Application;
using Updog.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Updog.Domain.Paging;

namespace Updog.Api {
    /// <summary>
    /// End point for managing posts.
    /// </summary>
    [Authorize]
    [Route("api/post")]
    [ApiController]
    public sealed class PostController : ApiController {
        #region Fields
        private IMediator mediator;
        #endregion

        #region Constructor(s)
        public PostController(IMediator mediator) {
            this.mediator = mediator;
        }
        #endregion

        #region Publics
        [AllowAnonymous]
        [HttpGet("new")]
        [ContentRangeHeader]
        public async Task<IActionResult> FindByNew([FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) =>
            (await mediator.Query<PostFindByNewQuery, PagedResultSet<PostReadView>>(
                new PostFindByNewQuery(new PaginationInfo(pageNumber, pageSize), User)
            )).Match(
                posts => Ok(posts) as IActionResult,
                error => BadRequest(error.Message)
            );

        /// <summary>
        /// Find a post via it's ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public async Task<IActionResult> FindById(int id) =>
            (await mediator.Query<PostFindByIdQuery, PagedResultSet<PostReadView>>(
                new PostFindByIdQuery(id, User)
            )).Match(
                post => Ok(post) as IActionResult,
                error => BadRequest(error.Message)
            );

        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet("{postId}/comment")]
        public async Task<IActionResult> FindComments(int postId) =>
            (await mediator.Query<CommentFindByPostQuery, IEnumerable<CommentReadView>>(
                new CommentFindByPostQuery(postId, User!)))
            .Match(
                comments => Ok(comments) as IActionResult,
                error => BadRequest(error.Message)
            );

        [AllowAnonymous]
        [ContentRangeHeader]
        [HttpGet("user/{username}")]
        public async Task<IActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) =>
            (await mediator.Query<PostFindByUserQuery, PagedResultSet<PostReadView>>(
                new PostFindByUserQuery(username, new PaginationInfo(pageNumber, pageSize), User)))
            .Match(
                posts => Ok(posts) as IActionResult,
                error => BadRequest(error.Message) as IActionResult
            );

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PostCreateRequest payload) =>
            (await mediator.Command(new PostCreateCommand(payload.Space, new PostCreate(payload.Type, payload.Title, payload.Body), User!)))
            .Match(
                r => Ok(new { Id = r.InsertId }) as IActionResult,
                e => BadRequest(e.Message)
            );

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]PostUpdateRequest payload) =>
            (await mediator.Command(new PostUpdateCommand(id, new PostUpdate(payload.Body), User!)))
            .Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );

        /// <summary>
        /// Delete a post.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            (await mediator.Command(new PostDeleteCommand(id, User!)))
            .Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );
        #endregion
    }
}