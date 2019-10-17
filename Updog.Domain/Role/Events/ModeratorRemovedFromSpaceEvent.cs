namespace Updog.Domain {
    public sealed class ModeratorRemovedFromSpaceEvent : IDomainEvent {
        #region Properties
        public Space Space { get; }
        public User User { get; }
        #endregion

        #region Constructor(s)
        public ModeratorRemovedFromSpaceEvent(Space space, User user) {
            Space = space;
            User = user;
        }
        #endregion
    }
}