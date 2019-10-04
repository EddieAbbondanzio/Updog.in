namespace Updog.Domain {
    public sealed class SpaceCreationData : IValueObject {
        #region Properties
        public string Name { get; }
        public string Description { get; }
        #endregion

        #region Constructor(s)
        public SpaceCreationData(string name, string description) {
            Name = name;
            Description = description;
        }
        #endregion
    }
}