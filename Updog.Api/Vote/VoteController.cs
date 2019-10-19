using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using Updog.Domain;

namespace Updog.Api {
    /// <summary>
    /// API controller to handle comments.
    /// </summary>
    [Authorize]
    [Route("api/vote")]
    [ApiController]
    public sealed class VoteController : ApiController {
        #region Fields
        private IMediator mediator;
        #endregion

        #region Constructor(s)
        public VoteController(IMediator mediator) {
            this.mediator = mediator;
        }
        #endregion

        /// <summary>
        /// Vote on a post.
        /// </summary>
        /// <param name="postId">The ID of the post to vote on.</param>
        /// <param name="vote">The vote type.</param>
        [HttpPost("post/{postId}/{vote}")]
        public async Task<IActionResult> VoteOnPost(int postId, VoteDirection vote) =>
            (await mediator.Command(new VoteOnPostCommand(new VoteOnPost(postId, vote), User!)))
            .Match(
                r => Ok(r) as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );

        /// <summary>
        /// Vote on a comment.
        /// </summary>
        /// <param name="commentId">The Id of the comment to vote on.</param>
        /// <param name="vote">The vote type.</param>
        [HttpPost("comment/{commentId}/{vote}")]
        public async Task<IActionResult> VoteOnComment(int commentId, VoteDirection vote) =>
            (await mediator.Command(new VoteOnCommentCommand(new VoteOnComment(commentId, vote), User!))).Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message) as IActionResult
            );
    }
}