namespace Updog.Domain {
    public sealed class SubscriptionDeleteEvent : IDomainEvent {
        #region Properties
        public Subscription Subscription { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteEvent(Subscription s) {
            Subscription = s;
        }
        #endregion
    }
}