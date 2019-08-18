using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Marker interface for now.
    /// </summary>
    public interface ICommentRepo : IRepo<Comment> {
        #region Publics
        /// <summary>
        /// Find the comments of a post.
        /// </summary>
        /// <param name="post">The post ID.</param>
        /// <returns>The comments associated with it.</returns>
        Task<Comment[]> FindByPost(int postId);
        #endregion
    }
}