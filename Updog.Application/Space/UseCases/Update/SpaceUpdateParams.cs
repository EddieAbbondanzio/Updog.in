using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a space.
    /// </summary>
    public sealed class SpaceUpdateParams {
        #region Properties
        /// <summary>
        /// The name of the space to update.
        /// </summary>
        /// <value></value>
        public string Name { get; }

        /// <summary>
        /// The new description of the space.
        /// </summary>
        public string Description { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdateParams(string name, string description, User user) {
            Name = name;
            Description = description;
            User = user;
        }
        #endregion
    }
}