using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain.Paging;

namespace Updog.Domain {
    /// <summary>
    /// Reader to get comments from the database in read view form.
    /// </summary>
    public interface ICommentReader : IReader<CommentReadView> {
        #region Publics
        /// <summary>
        /// Find a comment via it's unique ID. Returns all children of the comment as well.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <param name="user">The user reading the comment</param>
        /// <returns>The comment found (if any).</returns>
        Task<CommentReadView?> FindById(int id, User? user = null);

        /// <summary>
        /// Find a collection of comments for a specific post.
        /// </summary>
        /// <param name="postId">The ID of the post it belongs to.</param>
        /// <param name="user">The user reading the comment</param>
        /// <returns>All related comments to the post.</returns>
        Task<IEnumerable<CommentReadView>> FindByPost(int postId, User? user = null);

        /// <summary>
        /// Find a collection of comments for a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="paging">Pagination info.</param>
        /// <returns>The page of comments found (if any).</returns>
        Task<PagedResultSet<CommentReadView>> FindByUser(string username, PaginationInfo paging, User? user = null);
        #endregion
    }
}