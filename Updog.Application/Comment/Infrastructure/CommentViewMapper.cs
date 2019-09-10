using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a comment to a comment view.
    /// </summary>
    public sealed class CommentViewMapper : ICommentViewMapper {
        #region Fields
        /// <summary>
        /// Mapper to convert a user to a user view.
        /// </summary>
        private IUserViewMapper userMapper;

        private IVoteViewMapper voteMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment view mapper.
        /// </summary>
        /// <param name="userMapper">The user mapper.</param>
        /// <param name="voteMapper">The vote mapper</param>
        public CommentViewMapper(IUserViewMapper userMapper, IVoteViewMapper voteMapper) {
            this.userMapper = userMapper;
            this.voteMapper = voteMapper;
        }
        #endregion

        #region Publics2
        public CommentView Map(Comment comment) {
            UserView u = userMapper.Map(comment.User);
            VoteView v = voteMapper.Map(comment.Vote);
            CommentView cv = new CommentView(comment.Id, u, comment.Body, comment.CreationDate, comment.WasUpdated, comment.WasDeleted, comment.Upvotes, comment.Downvotes, v);

            if (comment.Children.Count > 0) {
                foreach (Comment c in comment.Children) {
                    cv.Children.Add(Map(c));
                }
            }

            return cv;
        }
        #endregion
    }
}