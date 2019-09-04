using Updog.Application;
using Updog.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Updog.Application.Paging;

namespace Updog.Api {
    /// <summary>
    /// End point for managing posts.
    /// </summary>
    [Authorize]
    [Route("api/post")]
    [ApiController]
    public sealed class PostController : ApiController {
        #region Fields
        private PostFinderById postFinderById;
        private PostFinderByNew postFinderByNew;
        private PostFinderByUser postFinderByUser;
        private CommentFinderByPost commentFinderByPost;
        private PostCreator postCreator;
        private PostUpdater postUpdater;

        private PostDeleter postDeleter;
        #endregion

        #region Constructor(s)
        public PostController(PostFinderById postFinderById, PostFinderByNew postFinderByNew, PostFinderByUser postFinderByUser, CommentFinderByPost commentFinderByPost, PostCreator postAdder, PostUpdater postUpdater, PostDeleter postDeleter) {
            this.postFinderById = postFinderById;
            this.postFinderByNew = postFinderByNew;
            this.postFinderByUser = postFinderByUser;
            this.commentFinderByPost = commentFinderByPost;
            this.postCreator = postAdder;
            this.postUpdater = postUpdater;
            this.postDeleter = postDeleter;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a post via it's ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public async Task<ActionResult> FindById(int id) {
            PostView p = await postFinderById.Handle(id);
            return p != null ? Ok(p) : NotFound() as ActionResult;
        }


        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet("{postId}/comment")]
        public async Task<ActionResult> GetComments(int postId) {
            IEnumerable<CommentView> comments = await commentFinderByPost.Handle(new CommentFinderByPostParams(postId));
            return Ok(comments);
        }

        /// <summary>
        /// Get new posts based on when they were made.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("new")]
        public async Task<ActionResult> FindByNew([FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            PagedResultSet<PostView> posts = await postFinderByNew.Handle(new PostFinderByNewParams(pageNumber, pageSize));
            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            PagedResultSet<PostView> posts = await postFinderByUser.Handle(new PostFinderByUserParam(username, pageNumber, pageSize));
            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostCreateRequest payload) {
            try {
                PostView post = await postCreator.Handle(new PostCreateParams(payload.Type, payload.Title, payload.Body, payload.Space, User));
                return Ok(post);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]PostUpdateRequest payload) {
            try {
                PostView p = await postUpdater.Handle(new PostUpdateParams(User, id, payload.Body));
                return Ok(p);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            try {
                PostView p = await postDeleter.Handle(new PostDeleteParams(User, id));
                return Ok(p);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}