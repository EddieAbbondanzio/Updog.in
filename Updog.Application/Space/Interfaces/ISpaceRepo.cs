using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// CRUD interface for managing spaces.
    /// </summary>
    public interface ISpaceRepo : IRepo<Space> {
        #region Publics
        /// <summary>
        /// Find a space via it's unique name.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        /// <returns>The found space. (if any)</returns>
        Task<Space> FindByName(string name);
        #endregion
    }
}