using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByUserQueryHandler : QueryHandler<CommentFindByUserQuery> {
        #region Fields
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentFindByUserQueryHandler(IDatabase database, ICommentViewMapper commentMapper) : base(database) {
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<CommentFindByUserQuery> context) {
            ICommentRepo commentRepo = context.Database.GetRepo<ICommentRepo>();

            PagedResultSet<Comment> comments = await commentRepo.FindByUser(context.Input.Username, context.Input.Paging?.PageNumber ?? 0, context.Input.Paging?.PageSize ?? Comment.PageSize);

            if (context.Input.User != null) {
                foreach (Comment c in comments) {
                    IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();
                    await GetVotes(voteRepo, c, context.Input.User);
                }
            }

            context.Output.Success(new PagedResultSet<CommentView>(
                comments.Select(c => commentMapper.Map(c)),
                comments.Pagination
            ));
        }
        #endregion

        #region Privates
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