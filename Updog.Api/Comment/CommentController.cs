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
            (await mediator.Query<CommentFindByIdQuery, CommentReadView?>(new CommentFindByIdQuery(commentId, User!)))
            .Match<IActionResult>(
                (comment) => comment != null ? Ok(comment) : NotFound() as IActionResult,
                (error) => BadRequest(error.Message)
            );

        [AllowAnonymous]
        [ContentRangeHeader]
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetCommentsByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery]int pageSize = Comment.PageSize) =>
            (await mediator.Query<CommentFindByUserQuery, PagedResultSet<CommentReadView>>(
                new CommentFindByUserQuery(username, new PaginationInfo(pageNumber, pageSize), User)
            )).Match<IActionResult>(
                (comments) => Ok(comments),
                (error) => BadRequest(error.Message)
            );

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody]CommentCreateRequest body) =>
            (await mediator.Command(new CommentCreateCommand(new CommentCreate(body.PostId, body.Body, body.ParentId), User!)))
            .Match<IActionResult>(
                (result) => Ok(new { Id = result.InsertId }),
                (error) => BadRequest(error)
            );

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<IActionResult> Update(int commentId, [FromBody]CommentUpdateRequest request) =>
            (await mediator.Command(new CommentUpdateCommand(commentId, new CommentUpdate(request.Body), User!)))
            .Match<IActionResult>(
                (result) => Ok(),
                (error) => BadRequest(error.Message)
            );

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId) =>
            (await mediator.Command(new CommentDeleteCommand(commentId, User!)))
            .Match<IActionResult>(
                (result) => Ok() as IActionResult,
                (error) => BadRequest(error.Message)
            );
        #endregion
    }
}