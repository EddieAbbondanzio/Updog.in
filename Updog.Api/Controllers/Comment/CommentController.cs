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
        private QueryHandler<CommentFindByIdQuery> commentFinderById;
        private QueryHandler<CommentFindByUserQuery> commentFinderByUser;
        private CommandHandler<CommentCreateCommand> commentCreator;
        private CommandHandler<CommentUpdateCommand> commentUpdater;
        private CommandHandler<CommentDeleteCommand> commentDeleter;
        #endregion

        #region Constructor(s)
        public CommentController(QueryHandler<CommentFindByIdQuery> commentFinderById, QueryHandler<CommentFindByUserQuery> commentFinderByUser, CommandHandler<CommentCreateCommand> commentCreator, CommandHandler<CommentUpdateCommand> commentUpdater, CommandHandler<CommentDeleteCommand> commentDeleter) {
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
            await commentFinderById.Execute(new CommentFindByIdQuery(commentId, User), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Comment.PageSize) {
            await commentFinderByUser.Execute(new CommentFindByUserQuery(username, User, new PaginationInfo(pageNumber, pageSize)), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            await commentCreator.Execute(new CommentCreateCommand(body.PostId, User!, body.Body, body.ParentId), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<ActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) {
            await commentUpdater.Execute(new CommentUpdateCommand(User!, commentId, request.Body), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId) {
            await commentDeleter.Execute(new CommentDeleteCommand(User!, commentId), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }
        #endregion
    }
}