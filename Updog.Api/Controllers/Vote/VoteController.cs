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
        private CommandHandler<VoteOnPostCommand> voteOnPostHandler;
        private CommandHandler<VoteOnCommentCommand> voteOnCommentHandler;
        #endregion

        #region Constructor(s)
        public VoteController(CommandHandler<VoteOnPostCommand> voteOnPostHandler, CommandHandler<VoteOnCommentCommand> voteOnCommentHandler) {
            this.voteOnPostHandler = voteOnPostHandler;
            this.voteOnCommentHandler = voteOnCommentHandler;
        }
        #endregion

        /// <summary>
        /// Vote on a post.
        /// </summary>
        /// <param name="postId">The ID of the post to vote on.</param>
        /// <param name="vote">The vote type.</param>
        [HttpPost("post/{postId}/{vote}")]
        public async Task<ActionResult> VoteOnPost(int postId, VoteDirection vote) {
            var result = await voteOnPostHandler.Execute(new VoteOnPostCommand(new VoteOnPost(postId, vote), User!));

            return Ok(result);
        }

        /// <summary>
        /// Vote on a comment.
        /// </summary>
        /// <param name="commentId">The Id of the comment to vote on.</param>
        /// <param name="vote">The vote type.</param>
        [HttpPost("comment/{commentId}/{vote}")]
        public async Task<ActionResult> VoteOnComment(int commentId, VoteDirection vote) {
            var result = await voteOnCommentHandler.Execute(new VoteOnCommentCommand(new VoteOnComment(commentId, vote), User!));
            return Ok(result);
        }
    }
}