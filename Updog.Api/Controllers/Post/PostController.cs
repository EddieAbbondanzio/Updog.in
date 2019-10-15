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
        public async Task<ActionResult> FindByNew([FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            var posts = await mediator.Query<PostFindByNewQuery, PagedResultSet<PostReadView>>(
                new PostFindByNewQuery(
                    new PaginationInfo(pageNumber, pageSize),
                    User
                )
            );

            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }

        /// <summary>
        /// Find a post via it's ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public async Task<ActionResult> FindById(int id) {
            var post = await mediator.Query<PostFindByIdQuery, PagedResultSet<PostReadView>>(
                new PostFindByIdQuery(id, User)
            );

            return post != null ? Ok(post) : NotFound() as ActionResult;
        }

        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet("{postId}/comment")]
        public async Task<ActionResult> FindComments(int postId) {
            var comments = await mediator.Query<CommentFindByPostQuery, IEnumerable<CommentReadView>>(
                new CommentFindByPostQuery(postId, User!)
            );

            return Ok(comments);
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            var posts = await mediator.Query<PostFindByUserQuery, PagedResultSet<PostReadView>>(
                new PostFindByUserQuery(username, new PaginationInfo(pageNumber, pageSize), User)
            );

            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostCreateRequest payload) {
            var result = await mediator.Command(new PostCreateCommand(payload.Space, new PostCreate(payload.Type, payload.Title, payload.Body), User!));

            return Ok(null!);
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]PostUpdateRequest payload) {
            var result = await mediator.Command(new PostUpdateCommand(id, new PostUpdate(payload.Body), User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await mediator.Command(new PostDeleteCommand(id, User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }
        #endregion
    }
}