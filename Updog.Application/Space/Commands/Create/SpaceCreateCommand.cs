using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommand : ICommand {
        #region Properties
        public SpaceCreationData CreationData { get; }

        /// <summary>
        /// The user performing the action.
        /// </summary>
        public User User { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreateCommand(SpaceCreationData creationData, User user) {
            CreationData = creationData;
            User = user;
        }
        #endregion
    }
}