using Updog.Application;
using Updog.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        private PostCreator postCreator;
        private PostUpdater postUpdater;

        private PostDeleter postDeleter;
        #endregion

        #region Constructor(s)
        public PostController(PostFinderById postFinderById, PostFinderByNew postFinderByNew, PostFinderByUser postFinderByUser, PostCreator postAdder, PostUpdater postUpdater, PostDeleter postDeleter) {
            this.postFinderById = postFinderById;
            this.postFinderByNew = postFinderByNew;
            this.postFinderByUser = postFinderByUser;
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
        /// Get new posts based on when they were made.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("new")]
        public async Task<ActionResult> FindByNew([FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            IEnumerable<PostView> posts = await postFinderByNew.Handle(new PaginationInfo(pageNumber, pageSize));
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            IEnumerable<PostView> posts = await postFinderByUser.Handle(new PostFinderByUserParam(username, new PaginationInfo(pageNumber, pageSize)));
            return Ok(posts);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostCreateRequest payload) {
            try {
                PostView post = await postCreator.Handle(new PostCreateParams(payload.Type, payload.Title, payload.Body, User));
                return Ok(post);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            } catch {
                return InternalServerError("An unknown error occured.");
            }
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]string body) {
            try {
                PostView p = await postUpdater.Handle(new PostUpdateParams(User, id, body));
                return Ok(p);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            } catch {
                return InternalServerError("An unknown error occured.");
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
            } catch {
                return InternalServerError("An unknown error occured.");
            }
        }
        #endregion
    }
}