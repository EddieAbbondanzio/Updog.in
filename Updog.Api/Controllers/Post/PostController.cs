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
        private QueryHandler<PostFindByNewQuery, PagedResultSet<PostReadView>> postFinderByNew;
        private QueryHandler<PostFindByIdQuery, PostReadView> postFinderById;
        private QueryHandler<PostFindByUserQuery, PagedResultSet<PostReadView>> postFinderByUser;
        private QueryHandler<CommentFindByPostQuery, IEnumerable<CommentReadView>> commentFinderByPost;
        private CommandHandler<PostCreateCommand> postCreator;
        private CommandHandler<PostUpdateCommand> postUpdater;
        private CommandHandler<PostDeleteCommand> postDeleter;
        #endregion

        #region Constructor(s)
        public PostController(
            QueryHandler<PostFindByNewQuery, PagedResultSet<PostReadView>> postFinderByNew,
            QueryHandler<PostFindByIdQuery, PostReadView> postFinderById,
            QueryHandler<PostFindByUserQuery, PagedResultSet<PostReadView>> postFinderByUser,
            QueryHandler<CommentFindByPostQuery, IEnumerable<CommentReadView>> commentFinderByPost,
            CommandHandler<PostCreateCommand> postAdder, CommandHandler<PostUpdateCommand> postUpdater,
            CommandHandler<PostDeleteCommand> postDeleter
        ) {
            this.postFinderByNew = postFinderByNew;
            this.postFinderById = postFinderById;
            this.postFinderByUser = postFinderByUser;
            this.commentFinderByPost = commentFinderByPost;
            this.postCreator = postAdder;
            this.postUpdater = postUpdater;
            this.postDeleter = postDeleter;
        }
        #endregion

        #region Publics
        [AllowAnonymous]
        [HttpGet("new")]
        public async Task<ActionResult> FindByNew([FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            var posts = await postFinderByNew.Execute(new PostFindByNewQuery() {
                User = User,
                Paging = new PaginationInfo(pageNumber, pageSize)
            });

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
            var post = await postFinderById.Execute(new PostFindByIdQuery() {
                PostId = id,
                User = User!
            });

            return post != null ? Ok(post) : NotFound() as ActionResult;
        }


        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet("{postId}/comment")]
        public async Task<ActionResult> FindComments(int postId) {
            var comments = await commentFinderByPost.Execute(new CommentFindByPostQuery() {
                PostId = postId,
                User = User
            });

            return Ok(comments);
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            var posts = await postFinderByUser.Execute(new PostFindByUserQuery() {
                Username = username,
                User = User,
                Paging = new PaginationInfo(pageNumber, pageSize)
            });

            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostCreateRequest payload) {
            var result = await postCreator.Execute(new PostCreateCommand(payload.Space, new PostCreate(payload.Type, payload.Title, payload.Body), User!));

            return Ok(null!);
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]PostUpdateRequest payload) {
            var result = await postUpdater.Execute(new PostUpdateCommand(id, new PostUpdate(payload.Body), User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await postDeleter.Execute(new PostDeleteCommand(id, User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }
        #endregion
    }
}