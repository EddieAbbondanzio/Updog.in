namespace Updog.Domain {
    public sealed class SpaceCreateData : IValueObject {
        #region Properties
        public string Name { get; }
        public string Description { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreateData(string name, string description) {
            Name = name;
            Description = description;
        }
        #endregion
    }
}