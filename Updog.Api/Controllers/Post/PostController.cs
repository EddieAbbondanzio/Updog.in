using Updog.Application;
using Updog.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Updog.Domain.Paging;

namespace Updog.Api {
    /// <summary>
    /// End point for managing posts.
    /// </summary>
    [Authorize]
    [Route("api/post")]
    [ApiController]
    public sealed class PostController : ApiController {
        #region Fields
        private QueryHandler<PostFindByNewQuery> postFinderByNew;
        private QueryHandler<PostFindByIdQuery> postFinderById;
        private QueryHandler<PostFindByUserQuery> postFinderByUser;
        private QueryHandler<CommentFindByPostQuery> commentFinderByPost;
        private CommandHandler<PostCreateCommand> postCreator;
        private CommandHandler<PostUpdateCommand> postUpdater;
        private CommandHandler<PostDeleteCommand> postDeleter;
        #endregion

        #region Constructor(s)
        public PostController(QueryHandler<PostFindByNewQuery> postFinderByNew, QueryHandler<PostFindByIdQuery> postFinderById, QueryHandler<PostFindByUserQuery> postFinderByUser, QueryHandler<CommentFindByPostQuery> commentFinderByPost, CommandHandler<PostCreateCommand> postAdder, CommandHandler<PostUpdateCommand> postUpdater, CommandHandler<PostDeleteCommand> postDeleter) {
            this.postFinderByNew = postFinderByNew;
            this.postFinderById = postFinderById;
            this.postFinderByUser = postFinderByUser;
            this.commentFinderByPost = commentFinderByPost;
            this.postCreator = postAdder;
            this.postUpdater = postUpdater;
            this.postDeleter = postDeleter;
        }
        #endregion

        #region Publics
        [AllowAnonymous]
        [HttpGet("new")]
        public async Task<ActionResult> FindByNew([FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            await postFinderByNew.Execute(new PostFindByNewQuery(User, new PaginationInfo(pageNumber, pageSize)), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Find a post via it's ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public async Task<ActionResult> FindById(int id) {
            await postFinderById.Execute(new PostFindByIdQuery(id, User), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }


        /// <summary>
        /// Get all the comments of a post.
        /// </summary>
        /// <param name="postId">The post ID.</param>
        [AllowAnonymous]
        [HttpGet("{postId}/comment")]
        public async Task<ActionResult> FindComments(int postId) {
            await commentFinderByPost.Execute(new CommentFindByPostQuery(postId, User), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult> FindByUser([FromRoute]string username, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            await postFinderByUser.Execute(new PostFindByUserQuery(username, User, new PaginationInfo(pageNumber, pageSize)), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostCreateRequest payload) {
            await postCreator.Execute(new PostCreateCommand(new PostCreationData(payload.Type, payload.Title, payload.Body, payload.Space), User!), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]PostUpdateRequest payload) {
            await postUpdater.Execute(new PostUpdateCommand(User!, id, payload.Body), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            await postDeleter.Execute(new PostDeleteCommand(User!, id), ActionResultBuilder);
            return ActionResultBuilder.Build();
        }
        #endregion
    }
}