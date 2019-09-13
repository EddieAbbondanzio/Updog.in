using System;
using System.Data.Common;
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new comment.
    /// </summary>
    public sealed class CommentCreator : IInteractor<CommentCreateParams, CommentView> {
        #region Fields
        private IDatabase database;
        private AbstractValidator<CommentCreateParams> commentValidator;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentCreator(IDatabase database, AbstractValidator<CommentCreateParams> commentValidator, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentValidator = commentValidator;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentCreateParams input) {
            await commentValidator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);

                // Locate the post to ensure it actually exists.
                Post? post = await postRepo.FindById(input.PostId);

                if (post == null) {
                    throw new InvalidOperationException();
                }

                using (var transaction = connection.BeginTransaction()) {
                    Comment comment = new Comment() {
                        User = input.User,
                        PostId = post.Id,
                        Body = input.Body,
                        CreationDate = DateTime.UtcNow
                    };

                    // Set the parent comment if needed.
                    if (input.ParentId != 0) {
                        comment.Parent = await commentRepo.FindById(input.ParentId);
                    }

                    // Update the comment count cache on the post.
                    post.CommentCount++;

                    comment.Upvotes++;
                    await commentRepo.Add(comment);

                    Vote upvote = new Vote() {
                        User = input.User,
                        ResourceId = comment.Id,
                        ResourceType = VoteResourceType.Comment,
                        Direction = VoteDirection.Up
                    };

                    await postRepo.Update(post);
                    await voteRepo.Add(upvote);

                    comment.Vote = upvote;
                    transaction.Commit();

                    return commentMapper.Map(comment);
                }
            }
        }
        #endregion
    }
}