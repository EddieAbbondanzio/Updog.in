namespace Updog.Domain {
    public sealed class SubscriptionCreateData : IValueObject {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionCreateData(string space) {
            Space = space;
        }
        #endregion
    }
}