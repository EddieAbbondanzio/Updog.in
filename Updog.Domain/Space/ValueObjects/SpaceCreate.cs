namespace Updog.Domain {
    public sealed class SpaceCreate : IValueObject {
        #region Properties
        public string Name { get; }
        public string Description { get; }

        public bool IsDefault { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreate(string name, string description, bool isDefault = false) {
            Name = name;
            Description = description;
            IsDefault = isDefault;
        }
        #endregion
    }
}