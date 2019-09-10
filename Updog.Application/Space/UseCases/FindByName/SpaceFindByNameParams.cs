using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindByNameParams : IAnonymousActionParams {
        #region Properties
        public string Name { get; }

        public User? User { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindByNameParams(string name, User? user = null) {
            Name = name;
            User = user;
        }
        #endregion
    }
}