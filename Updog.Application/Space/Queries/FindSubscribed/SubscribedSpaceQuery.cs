
namespace Updog.Application {
    public sealed class SubscribedSpaceQuery : IQuery {
        #region Properties
        public string Username { get; }
        #endregion

        #region Constructor(s)
        public SubscribedSpaceQuery(string username) {
            Username = username;
        }
        #endregion
    }
}