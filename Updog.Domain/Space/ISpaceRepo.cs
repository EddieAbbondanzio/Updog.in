using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Domain {
    /// <summary>
    /// CRUD interface for managing spaces.
    /// </summary>
    public interface ISpaceRepo : IRepo<Space> {
        #region Publics
        /// <summary>
        /// Get a list of spaces.
        /// </summary>
        /// <param name="pageNumber">The 0 based index of the page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The pages found.</returns>
        Task<PagedResultSet<Space>> Find(int pageNumber, int pageSize);

        /// <summary>
        /// Find a space via it's unique name.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        /// <returns>The found space. (if any)</returns>
        Task<Space?> FindByName(string name);

        /// <summary>
        /// Get all of the default spaces.
        /// </summary>
        /// <returns>The list of default spaces.</returns>
        Task<IEnumerable<Space>> FindDefault();

        /// <summary>
        /// Get all of the susbcribed spaces of the user.
        /// </summary>
        /// <returns>The list of spaces subscribed by the user.</returns>
        Task<IEnumerable<Space>> FindSubscribed(User user);
        #endregion
    }
}