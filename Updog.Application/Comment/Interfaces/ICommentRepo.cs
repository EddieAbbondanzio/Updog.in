using System.Collections.Generic;
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
        Task<IEnumerable<Comment>> FindByPost(int postId);

        /// <summary>
        /// Find a page of comments made by a specific user.
        /// </summary>
        /// <param name="username">The user to look for.</param>
        /// <param name="paginationInfo">Paging info.</param>
        /// <returns>The comments found.</returns>
        Task<IEnumerable<Comment>> FindByUser(string username, PaginationInfo paginationInfo);
        #endregion
    }
}