using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;
using Updog.Application.Paging;

namespace Updog.Application {
    /// <summary>
    /// CRUD interface for managing pots in the database.
    /// </summary>
    public interface IPostRepo : IRepo<Post> {
        /// <summary>
        /// Find posts for a specific user.
        /// </summary>
        /// <param name="username">THe username to look for.</param>
        /// <param name="pageNumber">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The collection of their posts (if any).</returns>
        Task<PagedResultSet<Post>> FindByUser(string username, int pageNumber, int pageSize);

        /// <summary>
        /// Find posts for a specific space.
        /// </summary>
        /// <param name="space">The space to get posts for.</param>
        /// <param name="pageNumber">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The collection of posts.</returns>
        Task<PagedResultSet<Post>> FindBySpace(string space, int pageNumber, int pageSize);

        /// <summary>
        /// Find posts by new for the front page.
        /// </summary>
        /// <param name="pageNumber">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The collection of posts.</returns>
        Task<PagedResultSet<Post>> FindByNew(int pageNumber, int pageSize);
    }
}