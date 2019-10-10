using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFindByNameQuery : AnonymousQuery {
        #region Properties
        public string Name { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindByNameQuery(string name, User? user = null) : base(user) {
            Name = name;
        }
        #endregion  
    }
}