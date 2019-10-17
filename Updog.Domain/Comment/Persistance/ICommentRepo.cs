using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Domain {
    /// <summary>
    /// Marker interface for now.
    /// </summary>
    public interface ICommentRepo : IRepo<Comment> {
        Task<bool> IsOwner(int commentId, string username);
    }
}