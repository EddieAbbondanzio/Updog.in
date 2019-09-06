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
        private PostFinderById _postFinderById;
        private PostFinderByUser _postFinderByUser;
        private CommentFinderByPost _commentFinderByPost;
        private PostCreator _postCreator;
        private PostUpdater _postUpdater;

        private PostDeleter _postDeleter;
        #endregion

        #region Constructor(s)
        public PostController(PostFinderById postFinderById, PostFinderByUser postFinderByUser, CommentFinderByPost commentFinderByPost, PostCreator postAdder, PostUpdater postUpdater, PostDeleter postDeleter) {
            this._postFinderById = postFinderById;
            this._postFinderByUser = postFinderByUser;
            this._commentFinderByPost = commentFinderByPost;
            this._postCreator = postAdder;
            this._postUpdater = postUpdater;
            this._postDeleter = postDeleter;
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
            PostView? p = await _postFinderById.Handle(id);
            return p != null ? Ok(p) : NotFound() as ActionResult;
        }


        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet("{postId}/comment")]
        public async Task<ActionResult> GetComments(int postId) {
            IEnumerable<CommentView> comments = await _commentFinderByPost.Handle(new CommentFinderByPostParams(postId));
            return Ok(comments);
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            PagedResultSet<PostView> posts = await _postFinderByUser.Handle(new PostFinderByUserParam(username, pageNumber, pageSize));
            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostCreateRequest payload) {
            PostView? post = await _postCreator.Handle(new PostCreateParams(payload.Type, payload.Title, payload.Body, payload.Space, User!));
            return Ok(post);

        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]PostUpdateRequest payload) {
            PostView p = await _postUpdater.Handle(new PostUpdateParams(User!, id, payload.Body));
            return Ok(p);

        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            PostView? p = await _postDeleter.Handle(new PostDeleteParams(User!, id));
            return Ok(p);
        }
        #endregion
    }
}