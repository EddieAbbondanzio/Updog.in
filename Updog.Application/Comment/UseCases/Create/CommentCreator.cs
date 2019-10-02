using System;
using System.Data.Common;
using System.Threading.Tasks;
using Updog.Domain;
using System.Reflection;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new comment.
    /// </summary>
    public sealed class CommentCreator : Interactor<CommentCreateParams, CommentView> {
        #region Fields
        private IDatabase database;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentCreator(IDatabase database, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(CommentCreateValidator))]
        protected async override Task<CommentView> HandleInput(CommentCreateParams input) {
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
                        ResourceType = VotableEntityType.Comment,
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