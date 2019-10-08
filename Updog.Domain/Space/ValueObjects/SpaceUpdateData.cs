namespace Updog.Domain {
    public sealed class SpaceUpdateData : IValueObject {
        #region Properties
        public string Space { get; }

        public string Description { get; }
        #endregion

        #region Constructor(s)
        public SpaceUpdateData(string space, string description) {
            Space = space;
            Description = description;
        }
        #endregion
    }
}