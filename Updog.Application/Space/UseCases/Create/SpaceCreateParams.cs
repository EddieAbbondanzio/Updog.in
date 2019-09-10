using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to create a new sub space.
    /// </summary>
    public sealed class SpaceCreateParams : IAuthenticatedActionParams {
        #region Properties
        /// <summary>
        /// The desired name of the subspace.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The description of the space.
        /// </summary>
        /// <value></value>
        public string Description { get; }

        /// <summary>
        /// The user performing the action.
        /// </summary>
        public User User { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreateParams(string name, string description, User user) {
            Name = name;
            Description = description;
            User = user;
        }
        #endregion
    }
}