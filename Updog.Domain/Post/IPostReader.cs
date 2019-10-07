using System.Threading.Tasks;
using Updog.Domain.Paging;

namespace Updog.Domain {
    public interface IPostReader : IReader<PostReadView> {
        /// <summary>
        /// Find posts for a specific user.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <param name="paging">Paging details.</param>
        /// <returns>The collection of their posts (if any).</returns>
        Task<PagedResultSet<PostReadView>> FindByUser(string username, PaginationInfo paging, User? user = null);

        /// <summary>
        /// Find posts for a specific space.
        /// </summary>
        /// <param name="space">The space to get posts for.</param>
        /// <param name="paging">Paging details.</param>
        /// <returns>The collection of posts.</returns>
        Task<PagedResultSet<PostReadView>> FindBySpace(string space, PaginationInfo paging, User? user = null);

        /// <summary>
        /// Find posts by new for the front page.
        /// </summary>
        /// <param name="paging">Paging details.</param>
        /// <returns>The collection of posts.</returns>
        Task<PagedResultSet<PostReadView>> FindByNew(PaginationInfo paging, User? user = null);
    }
}