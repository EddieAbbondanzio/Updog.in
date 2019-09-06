using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// CRUD interface for managing subscriptions in the database.
    /// </summary>
    public interface ISubscriptionRepo : IRepo<Subscription> {
        /// <summary>
        /// Find all the subscriptions for a user.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The subscriptions found.</returns>
        Task<IEnumerable<Subscription>> FindByUser(string username);

        /// <summary>
        /// Find a specific subscription by it's user (owner) and space.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        /// <param name="space">The space it's for.</param>
        /// <returns>The subscription found (if any).</returns>
        Task<Subscription?> FindByUserAndSpace(string username, string space);
    }
}