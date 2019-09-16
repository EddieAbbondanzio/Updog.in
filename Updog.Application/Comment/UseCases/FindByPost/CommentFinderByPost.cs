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
    public sealed class CommentFinderByPost : IInteractor<FindByValueParams<int>, IEnumerable<CommentView>> {
        #region Fields
        private IDatabase database;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentFinderByPost(IDatabase database, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<IEnumerable<CommentView>> Handle(FindByValueParams<int> input) {
            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);

                IEnumerable<Comment> comments = await commentRepo.FindByPost(input.Value);

                if (input.User != null) {
                    IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);

                    foreach (Comment c in comments) {
                        await GetVotes(voteRepo, c, input.User);
                    }
                }

                return comments.Select(c => commentMapper.Map(c));
            }
        }

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