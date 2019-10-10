using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Api {
    /// <summary>
    /// API controller to handle comments.
    /// </summary>
    [Authorize]
    [Route("api/comment")]
    [ApiController]
    public sealed class CommentController : ApiController {
        #region Fields
        private QueryHandler<CommentFindByIdQuery, CommentReadView> commentFinderById;
        private QueryHandler<CommentFindByUserQuery, PagedResultSet<CommentReadView>> commentFinderByUser;
        private CommandHandler<CommentCreateCommand> commentCreator;
        private CommandHandler<CommentUpdateCommand> commentUpdater;
        private CommandHandler<CommentDeleteCommand> commentDeleter;
        #endregion

        #region Constructor(s)
        public CommentController(QueryHandler<CommentFindByIdQuery, CommentReadView> commentFinderById, QueryHandler<CommentFindByUserQuery, PagedResultSet<CommentReadView>> commentFinderByUser, CommandHandler<CommentCreateCommand> commentCreator, CommandHandler<CommentUpdateCommand> commentUpdater, CommandHandler<CommentDeleteCommand> commentDeleter) {
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
        public async Task<IActionResult> GetComment(int commentId) {
            CommentReadView? comment = await commentFinderById.Execute(new CommentFindByIdQuery(commentId, User!));
            return comment != null ? Ok(comment) : NotFound() as IActionResult;
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Comment.PageSize) {
            PagedResultSet<CommentReadView> comments = await commentFinderByUser.Execute(new CommentFindByUserQuery(username, new PaginationInfo(pageNumber, pageSize), User));

            SetContentRangeHeader(comments.Pagination);
            return Ok(comments);
        }

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            var result = await commentCreator.Execute(new CommentCreateCommand(new CommentCreate(body.PostId, body.Body, body.ParentId), User!));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<IActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) {
            var result = await commentUpdater.Execute(new CommentUpdateCommand(commentId, new CommentUpdate(request.Body), User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId) {
            var result = await commentDeleter.Execute(new CommentDeleteCommand(commentId, User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }
        #endregion
    }
}