using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommand : ICommand {
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
        public SpaceCreateCommand(User user, string name, string description) {
            Name = name;
            Description = description;
            User = user;
        }
        #endregion
    }
}