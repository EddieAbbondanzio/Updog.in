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
    [Route("api/vote")]
    [ApiController]
    public sealed class VoteController : ApiController {
        #region Fields
        private PostVoter postVoter;
        private CommentVoter commentVoter;
        #endregion

        #region Constructor(s)
        public VoteController(PostVoter postVoter, CommentVoter commentVoter) {
            this.postVoter = postVoter;
            this.commentVoter = commentVoter;
        }
        #endregion

        /// <summary>
        /// Vote on a post.
        /// </summary>
        /// <param name="postId">The ID of the post to vote on.</param>
        /// <param name="vote">The vote type.</param>
        [HttpPost("post/{postId}/{vote}")]
        public async Task<ActionResult> VoteOnPost(int postId, VoteDirection vote) {
            var result = await postVoter.Handle(new VoteOnPostParams(postId, vote, User!));

            return result.Match<ActionResult>(
                vote => Ok(vote),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Vote on a comment.
        /// </summary>
        /// <param name="commentId">The Id of the comment to vote on.</param>
        /// <param name="vote">The vote type.</param>
        [HttpPost("comment/{commentId}/{vote}")]
        public async Task<ActionResult> VoteOnComment(int commentId, VoteDirection vote) {
            var result = await commentVoter.Handle(new VoteOnCommentParams(commentId, vote, User!));

            return result.Match<ActionResult>(
                vote => Ok(vote),
                fail => BadRequest(fail)
            );
        }
    }
}