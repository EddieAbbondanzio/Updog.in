using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public interface IPostRepo : IRepo<Post> {
        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns></returns>
        Task<PostInfo[]> FindNewest(int pageNumber, int pageSize);
    }
}