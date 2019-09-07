using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// CRUD interface for managing votes in the database.
    /// </summary>
    public interface IVoteRepo : IRepo<Vote> {
        /// <summary>
        /// Find a vote for a specific post and user.
        /// </summary>
        /// <param name="username">The username of the user that cast it..</param>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>The vote found (if any).</returns>
        Task<Vote?> FindByUserAndPost(string username, int postId);

        /// <summary>
        /// Find a vote for a specific comment and user.
        /// </summary>
        /// <param name="username">The username of the user that cast it.</param>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns></returns>
        Task<Vote?> FindByUserAndComment(string username, int commentId);
    }
}