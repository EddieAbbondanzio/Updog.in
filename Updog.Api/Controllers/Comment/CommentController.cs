using System;
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
    [Route("api/comment")]
    [ApiController]
    public sealed class CommentController : ApiController {
        #region Fields
        private CommentFinderById commentFinderById;

        private CommentFinderByPost commentFinderByPost;

        private CommentCreator commentCreator;

        private CommentUpdater commentUpdater;

        private CommentDeleter commentDeleter;
        #endregion

        #region Constructor(s)
        public CommentController(CommentFinderById commentFinderById, CommentFinderByPost commentFinderByPost, CommentCreator commentCreator, CommentUpdater commentUpdater, CommentDeleter commentDeleter) {
            this.commentFinderById = commentFinderById;
            this.commentFinderByPost = commentFinderByPost;
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
            CommentView c = await commentFinderById.Handle(commentId);
            return c != null ? Ok(c) : NotFound() as ActionResult;
        }

        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetComments([FromQuery]int postId) {
            try {
                CommentView[] comments = await commentFinderByPost.Handle(postId);
                return Ok(comments);
            } catch (Exception e) {
                Console.Write(e);
                throw e;
            }
        }

        /// <summary>
        /// Create a new comment on a post.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody]CommentCreateRequest body) {
            try {
                CommentView comment = await commentCreator.Handle(new CommentCreateParams(body.PostId, User, body.Body, body.ParentId));
                return Ok(comment);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            } catch {
                return InternalServerError("An unknown error occured.");
            }
        }

        /// <summary>
        /// Edit a comment.
        /// </summary>
        [HttpPatch("{commentId}")]
        public async Task<ActionResult> Update(int commentId, [FromBody]string body) {
            try {
                Comment c = await commentUpdater.Handle(new CommentUpdateParams(User, commentId, body));
                return Ok(c);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            } catch {
                return InternalServerError("An unknown error occured.");
            }
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId) {
            try {
                Comment c = await commentDeleter.Handle(new CommentDeleteParams(User, commentId));
                return Ok(c);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            } catch {
                return InternalServerError("An unknown error occured.");
            }
        }
        #endregion
    }
}