using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
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
        private CommentFinderById _commentFinderById;

        private CommentFinderByUser _commentFinderByUser;

        private CommentCreator _commentCreator;

        private CommentUpdater _commentUpdater;

        private CommentDeleter _commentDeleter;
        #endregion

        #region Constructor(s)
        public CommentController(CommentFinderById commentFinderById, CommentFinderByUser commentFinderByUser, CommentCreator commentCreator, CommentUpdater commentUpdater, CommentDeleter commentDeleter) {
            this._commentFinderById = commentFinderById;
            this._commentFinderByUser = commentFinderByUser;
            this._commentCreator = commentCreator;
            this._commentUpdater = commentUpdater;
            this._commentDeleter = commentDeleter;
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
            CommentView? c = await _commentFinderById.Handle(new FindByValueParams<int>(commentId, User));
            return c != null ? Ok(c) : NotFound() as ActionResult;
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Post.PageSize) {
            PagedResultSet<CommentView> comments = await _commentFinderByUser.Handle(new FindByValueParams<string>(username, User, new PaginationInfo(pageNumber, pageSize)));
            SetContentRangeHeader(comments.Pagination);
            return Ok(comments);
        }

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            Either<CommentView, ValidationResult> result = await _commentCreator.Handle(new CommentCreateParams(body.PostId, User!, body.Body, body.ParentId));

            return result.Match<ActionResult>(
                (CommentView comment) => Ok(comment),
                (ValidationResult valResult) => ValidationFailure(valResult)
            );
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<ActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) {
            CommentView c = await _commentUpdater.Handle(new CommentUpdateParams(User!, commentId, request.Body));
            return Ok(c);
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId) {
            CommentView c = await _commentDeleter.Handle(new CommentDeleteParams(User!, commentId));
            return Ok(c);
        }
        #endregion
    }
}