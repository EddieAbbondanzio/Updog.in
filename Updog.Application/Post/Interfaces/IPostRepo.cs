using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public interface IPostRepo : IRepo<Post> {
        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pagination">Paging info</param>
        /// <returns>The newest new posts.</returns>
        Task<Post[]> FindNewest(PaginationInfo pagination);
    }
}