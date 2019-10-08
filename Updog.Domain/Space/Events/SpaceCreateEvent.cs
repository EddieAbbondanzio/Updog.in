namespace Updog.Domain {
    public sealed class SpaceCreateEvent : IDomainEvent {
        #region Properties
        public Space Space { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreateEvent(Space space) {
            Space = space;
        }
        #endregion
    }
}