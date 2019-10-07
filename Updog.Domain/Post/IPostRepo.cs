using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Domain {
    /// <summary>
    /// CRUD interface for managing pots in the database.
    /// </summary>
    public interface IPostRepo : IRepo<Post> {
    }
}