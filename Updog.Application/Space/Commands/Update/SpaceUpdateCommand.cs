using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a space.
    /// </summary>
    public sealed class SpaceUpdateCommand : ICommand {
        #region Properties
        public string Name { get; }

        public string Description { get; }

        public User User { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdateCommand(User user, string name, string description) {
            Name = name;
            Description = description;
            User = user;
        }
        #endregion
    }
}