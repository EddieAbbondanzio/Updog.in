using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find comments on a post.
    /// </summary>
    public sealed class CommentFinderByUser : Interactor<FindByValueParams<string>, PagedResultSet<CommentView>> {
        #region Fields
        private IDatabase database;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentFinderByUser(IDatabase database, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(FindByUserValidator))]
        protected async override Task<PagedResultSet<CommentView>> HandleInput(FindByValueParams<string> input) {
            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);

                PagedResultSet<Comment> comments = await commentRepo.FindByUser(input.Value, input.Pagination?.PageNumber ?? 0, input.Pagination?.PageSize ?? Comment.PageSize);

                if (input.User != null) {
                    foreach (Comment c in comments) {
                        IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);
                        await GetVotes(voteRepo, c, input.User);
                    }
                }

                return new PagedResultSet<CommentView>(
                    comments.Select(c => commentMapper.Map(c)),
                    comments.Pagination
                );
            }
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Recursive helper to get the votes for all children.
        /// </summary>
        private async Task GetVotes(IVoteRepo voteRepo, Comment comment, User user) {
            comment.Vote = await voteRepo.FindByUserAndComment(user.Username, comment.Id);

            foreach (Comment child in comment.Children) {
                await GetVotes(voteRepo, child, user);
            }
        }
        #endregion
    }
}