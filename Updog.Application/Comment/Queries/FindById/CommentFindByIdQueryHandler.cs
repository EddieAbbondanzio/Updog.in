using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByIdQueryHandler : QueryHandler<CommentFindByIdQuery> {
        #region Fields
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentFindByIdQueryHandler(IDatabase database, ICommentViewMapper commentMapper) : base(database) {
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<CommentFindByIdQuery> context) {
            ICommentRepo commentRepo = context.Database.GetRepo<ICommentRepo>();

            Comment? c = await commentRepo.FindById(context.Input.CommentId);

            if (c == null) {
                context.Output.NotFound();
                return;
            }

            if (context.Input.User != null) {
                IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();
                await GetVotes(voteRepo, c, context.Input.User);
            }

            context.Output.Success(commentMapper.Map(c));
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