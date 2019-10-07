using System.Linq;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a post into it's data transfer object.
    /// </summary>
    public sealed class PostViewMapper : IPostViewMapper {
        #region Fields
        /// <summary>
        /// Mapper to convert a user to its DTO.
        /// </summary>
        private IUserViewMapper userMapper;

        private ISpaceViewMapper spaceMapper;

        private IVoteViewMapper voteMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post view mapper.
        /// </summary>
        /// <param name="userMapper">The mapper to convert users to user views.</param>
        /// <param name="spaceMapper">The mapper to convert space entities</param>
        /// <param name="voteMapper">The vote view mapper.</param>
        public PostViewMapper(IUserViewMapper userMapper, ISpaceViewMapper spaceMapper, IVoteViewMapper voteMapper) {
            this.userMapper = userMapper;
            this.spaceMapper = spaceMapper;
            this.voteMapper = voteMapper;
        }
        #endregion

        #region Publics
        public PostView Map(Post post) {
            return new PostView(post.Id, post.Type, post.Title, post.Body, null!, null!, post.CreationDate, post.CommentCount, post.WasUpdated, post.WasDeleted, post.Votes.Upvotes, post.Votes.Downvotes, null!);
        }
        #endregion
    }
}