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
        private PostFinderByNew postFinderByNew;
        private PostFinderById postFinderById;
        private PostFinderByUser postFinderByUser;
        private CommentFinderByPost commentFinderByPost;
        private PostCreator postCreator;
        private PostUpdater postUpdater;
        private PostDeleter postDeleter;
        #endregion

        #region Constructor(s)
        public PostController(PostFinderByNew postFinderByNew, PostFinderById postFinderById, PostFinderByUser postFinderByUser, CommentFinderByPost commentFinderByPost, PostCreator postAdder, PostUpdater postUpdater, PostDeleter postDeleter) {
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
            var findResult = await postFinderByNew.Handle(new FindParams(User, new PaginationInfo(pageSize, pageNumber)));

            return findResult.Match<ActionResult>(
                posts => {
                    SetContentRangeHeader(posts.Pagination);
                    return Ok(posts);
                },
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Find a post via it's ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public async Task<ActionResult> FindById(int id) {
            var findResult = await postFinderById.Handle(new FindByValueParams<int>(id, User));

            return findResult.Match<ActionResult>(
                post => post != null ? Ok(post) : NotFound() as ActionResult,
                fail => BadRequest(fail)
            );
        }


        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet("{postId}/comment")]
        public async Task<ActionResult> FindComments(int postId) {
            var findResult = await commentFinderByPost.Handle(new FindByValueParams<int>(postId, User));

            return findResult.Match<ActionResult>(
                comments => Ok(comments),
                fail => BadRequest(fail)
            );
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            var findResult = await postFinderByUser.Handle(new FindByValueParams<string>(username, User, new PaginationInfo(pageNumber, pageSize)));

            return findResult.Match<ActionResult>(
                posts => {
                    SetContentRangeHeader(posts.Pagination);
                    return Ok(posts);
                },
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostCreateRequest payload) {
            var createResult = await postCreator.Handle(new PostCreateParams(payload.Type, payload.Title, payload.Body, payload.Space, User!));

            return createResult.Match<ActionResult>(
                newPost => Ok(newPost),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]PostUpdateRequest payload) {
            var updateResult = await postUpdater.Handle(new PostUpdateParams(User!, id, payload.Body));

            return updateResult.Match<ActionResult>(
                updatedPost => Ok(updatedPost),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var deleteResult = await postDeleter.Handle(new PostDeleteParams(User!, id));

            return deleteResult.Match<ActionResult>(
                deletedPost => Ok(deletedPost),
                fail => BadRequest(fail)
            );
        }
        #endregion
    }
}