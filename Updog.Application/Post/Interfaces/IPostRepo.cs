using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// CRUD interface for managing pots in the database.
    /// </summary>
    public interface IPostRepo : IRepo<Post> {
        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pagination">Paging info</param>
        /// <returns>The newest new posts.</returns>
        Task<Post[]> FindNewest(PaginationInfo pagination);

        /// <summary>
        /// Find posts for a specific user.
        /// </summary>
        /// <param name="username">THe username to look for.</param>
        /// <param name="pagination">Paging info</param>
        /// <returns>The collection of their posts (if any).</returns>
        Task<Post[]> FindByUser(string username, PaginationInfo pagination);
    }
}