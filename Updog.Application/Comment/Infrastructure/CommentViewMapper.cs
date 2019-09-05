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
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment view mapper.
        /// </summary>
        /// <param name="userMapper">The user mapper.</param>
        public CommentViewMapper(IUserViewMapper userMapper) {
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics2
        public CommentView Map(Comment comment) {
            UserView u = userMapper.Map(comment.User);
            CommentView cv = new CommentView(comment.Id, u, comment.Body, comment.CreationDate, comment.WasUpdated, comment.WasDeleted);

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