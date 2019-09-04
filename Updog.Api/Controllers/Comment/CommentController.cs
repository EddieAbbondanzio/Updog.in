using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Api {
    /// <summary>
    /// API controller to handle comments.
    /// </summary>
    [Authorize]
    [Route("api/comment")]
    [ApiController]
    public sealed class CommentController : ApiController {
        #region Fields
        private CommentFinderById commentFinderById;

        private CommentFinderByUser commentFinderByUser;

        private CommentCreator commentCreator;

        private CommentUpdater commentUpdater;

        private CommentDeleter commentDeleter;
        #endregion

        #region Constructor(s)
        public CommentController(CommentFinderById commentFinderById, CommentFinderByUser commentFinderByUser, CommentCreator commentCreator, CommentUpdater commentUpdater, CommentDeleter commentDeleter) {
            this.commentFinderById = commentFinderById;
            this.commentFinderByUser = commentFinderByUser;
            this.commentCreator = commentCreator;
            this.commentUpdater = commentUpdater;
            this.commentDeleter = commentDeleter;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a specific comment.
        /// </summary>
        /// <param name="commentId"></param>
        [AllowAnonymous]
        [HttpGet("{commentId}")]
        public async Task<ActionResult> GetComment(int commentId) {
            CommentView c = await commentFinderById.Handle(commentId);
            return c != null ? Ok(c) : NotFound() as ActionResult;
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Post.PageSize) {
            PagedResultSet<CommentView> comments = await commentFinderByUser.Handle(new CommentFinderByUserParams(username, pageNumber, pageSize));
            SetContentRangeHeader(comments.Pagination);
            return Ok(comments);
        }

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            try {
                CommentView comment = await commentCreator.Handle(new CommentCreateParams(body.PostId, User, body.Body, body.ParentId));
                return Ok(comment);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<ActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) {
            try {
                CommentView c = await commentUpdater.Handle(new CommentUpdateParams(User, commentId, request.Body));
                return Ok(c);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId) {
            try {
                CommentView c = await commentDeleter.Handle(new CommentDeleteParams(User, commentId));
                return Ok(c);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}