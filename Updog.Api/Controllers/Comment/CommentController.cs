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
            CommentReadView? comment = await commentFinderById.Execute(new CommentFindByIdQuery() { CommentId = commentId, User = User });
            return comment != null ? Ok(comment) : NotFound() as IActionResult;
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Comment.PageSize) {
            PagedResultSet<CommentReadView> comments = await commentFinderByUser.Execute(new CommentFindByUserQuery() { Username = username, User = User, Paging = new PaginationInfo(pageNumber, pageSize) });

            SetContentRangeHeader(comments.Pagination);
            return Ok(comments);
        }

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            var result = await commentCreator.Execute(new CommentCreateCommand() { CreationData = new CommentCreateData(body.PostId, body.Body, body.ParentId), User = User! });
            return Ok(result);
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<ActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) {
            await commentUpdater.Execute(new CommentUpdateCommand() { User = User!, CommentId = commentId, Body = request.Body });
            return Ok();
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId) {
            await commentDeleter.Execute(new CommentDeleteCommand() { User = User!, CommentId = commentId });
            return Ok();
        }
        #endregion
    }
}