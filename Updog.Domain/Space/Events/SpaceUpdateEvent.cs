namespace Updog.Domain {
    public sealed class SpaceUpdateEvent : IDomainEvent {
        #region Properties
        public Space Space { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdateEvent(Space space) {
            Space = space;
        }
        #endregion
    }
}