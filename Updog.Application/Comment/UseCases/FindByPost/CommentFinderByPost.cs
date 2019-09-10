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
    public sealed class CommentFinderByPost : IInteractor<CommentFinderByPostParams, IEnumerable<CommentView>> {
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
        public async Task<IEnumerable<CommentView>> Handle(CommentFinderByPostParams p) {
            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);

                IEnumerable<Comment> comments = await commentRepo.FindByPost(p.PostId);

                if (p.User != null) {
                    foreach (Comment c in comments) {
                        IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);
                        await GetVotes(voteRepo, c, p.User);
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