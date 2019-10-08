namespace Updog.Domain {
    public sealed class SubscriptionCreateEvent : IDomainEvent {
        #region Properties
        public Subscription Subscription { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionCreateEvent(Subscription s) {
            Subscription = s;
        }
        #endregion
    }
}