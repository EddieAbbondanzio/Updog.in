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
        private IMediator mediator;
        #endregion

        #region Constructor(s)
        public CommentController(IMediator mediator) {
            this.mediator = mediator;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a specific comment.
        /// </summary>
        /// <param name="commentId"></param>
        [AllowAnonymous]
        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetComment(int commentId) =>
            (await mediator.Query<CommentFindByIdQuery, CommentReadView>(
                new CommentFindByIdQuery(commentId, User!)
            )).Match(
                (comment) => Ok(comment) as IActionResult,
                (error) => BadRequest(error.Message) as IActionResult
            );

        [AllowAnonymous]
        [ContentRangeHeader]
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Comment.PageSize) =>
            (await mediator.Query<CommentFindByUserQuery, PagedResultSet<CommentReadView>>(
                new CommentFindByUserQuery(username, new PaginationInfo(pageNumber, pageSize), User)
            )).Match(
                (comments) => Ok(comments) as IActionResult,
                (error) => BadRequest(error.Message) as IActionResult
            );

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            var result = await mediator.Command(new CommentCreateCommand(new CommentCreate(body.PostId, body.Body, body.ParentId), User!));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<IActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) {
            var result = await mediator.Command(new CommentUpdateCommand(commentId, new CommentUpdate(request.Body), User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId) {
            var result = await mediator.Command(new CommentDeleteCommand(commentId, User!));
            return result.IsSuccess ? Ok() : BadRequest(result.Error) as IActionResult;
        }
        #endregion
    }
}