using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Domain {
    /// <summary>
    /// CRUD interface for managing pots in the database.
    /// </summary>
    public interface IPostRepo : IRepo<Post> {
        Task<bool> IsOwner(int postId, string username);
        Task<bool> Exists(int postId);
    }
}