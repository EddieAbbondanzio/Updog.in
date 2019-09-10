using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a comment by it's ID.
    /// </summary>
    public sealed class CommentFinderById : IInteractor<CommentFindByIdParams, CommentView?> {
        #region Fields
        private IDatabase database;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentFinderById(IDatabase database, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView?> Handle(CommentFindByIdParams input) {
            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);

                Comment? c = await commentRepo.FindById(input.CommentId);
                return c != null ? commentMapper.Map(c) : null;
            }
        }
        #endregion
    }
}