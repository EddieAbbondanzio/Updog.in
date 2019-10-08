namespace Updog.Domain {
    public sealed class SubscriptionDeleteData : IValueObject {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteData(string space) {
            Space = space;
        }
        #endregion
    }
}