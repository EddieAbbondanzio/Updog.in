using System.Linq;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a post into it's data transfer object.
    /// </summary>
    public sealed class PostViewMapper : IMapper<Post, PostView> {
        #region Fields
        /// <summary>
        /// Mapper to convert a user to its DTO.
        /// </summary>
        private IMapper<User, UserView> userMapper;

        /// <summary>
        /// Mapper to convert a comment into it's DTO.
        /// </summary>
        private IMapper<Comment, CommentView> commentMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post view mapper.
        /// </summary>
        /// <param name="userMapper">The mapper to convert users to user views.</param>
        /// <param name="commentMapper">The mapper to convert comments to their view.</param>
        public PostViewMapper(IMapper<User, UserView> userMapper, IMapper<Comment, CommentView> commentMapper) {
            this.userMapper = userMapper;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public PostView Map(Post post) {
            UserView userView = userMapper.Map(post.User);
            return new PostView(post.Id, post.Type, post.Title, post.Body, userView, post.CreationDate, post.CommentCount);
        }
        #endregion
    }
}