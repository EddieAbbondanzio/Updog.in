namespace Updog.Domain {
    public sealed class SpaceUpdate : IValueObject {
        #region Properties
        public string Description { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdate(string description) {
            Description = description;
        }
        #endregion
    }
}