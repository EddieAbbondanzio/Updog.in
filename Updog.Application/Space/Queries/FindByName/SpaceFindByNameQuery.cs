namespace Updog.Application {
    public sealed class SpaceFindByNameQuery : IQuery {
        #region Properties
        public string Name { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindByNameQuery(string name) {
            Name = name;
        }
        #endregion
    }
}