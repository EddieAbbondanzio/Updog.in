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
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pageNumber">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The newest new posts.</returns>
        Task<PagedResultSet<Post>> FindNewest(int pageNumber, int pageSize);

        /// <summary>
        /// Find posts for a specific user.
        /// </summary>
        /// <param name="username">THe username to look for.</param>
        /// <param name="pageNumber">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The collection of their posts (if any).</returns>
        Task<PagedResultSet<Post>> FindByUser(string username, int pageNumber, int pageSize);
    }
}