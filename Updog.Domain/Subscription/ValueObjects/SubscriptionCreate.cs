namespace Updog.Domain {
    public sealed class SubscriptionCreate : IValueObject {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionCreate(string space) {
            Space = space;
        }
        #endregion
    }
}