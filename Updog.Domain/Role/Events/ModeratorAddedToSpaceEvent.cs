namespace Updog.Domain {
    public sealed class ModeratorAddedToSpaceEvent : IDomainEvent {
        #region Properties
        public Space Space { get; }
        public User User { get; }
        #endregion

        #region Constructor(s)
        public ModeratorAddedToSpaceEvent(Space space, User user) {
            Space = space;
            User = user;
        }
        #endregion
    }
}