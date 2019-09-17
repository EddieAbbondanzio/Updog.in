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
            var findResult = await commentFinderById.Handle(new FindByValueParams<int>(commentId, User));

            return findResult.Match(
                comment => comment != null ? Ok(comment) : NotFound() as ActionResult,
                fail => BadRequest(fail)
            );
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Comment.PageSize) {
            var findResult = await commentFinderByUser.Handle(new FindByValueParams<string>(username, User, new PaginationInfo(pageNumber, pageSize)));

            return findResult.Match<ActionResult>(
                comments => {
                    SetContentRangeHeader(comments.Pagination);
                    return Ok(comments);
                },
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            var result = await commentCreator.Handle(new CommentCreateParams(body.PostId, User!, body.Body, body.ParentId));

            return result.Match<ActionResult>(
                comment => Ok(comment),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<ActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) {
            var updateResult = await commentUpdater.Handle(new CommentUpdateParams(User!, commentId, request.Body));

            return updateResult.Match<ActionResult>(
                updatedComment => Ok(updatedComment),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId) {
            var deleteResult = await commentDeleter.Handle(new CommentDeleteParams(User!, commentId));

            return deleteResult.Match<ActionResult>(
                deletedComment => Ok(deletedComment),
                fail => BadRequest(fail)
            );
        }
        #endregion
    }
}