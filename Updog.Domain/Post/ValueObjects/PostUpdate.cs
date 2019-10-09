namespace Updog.Domain {
    public sealed class PostUpdate : IValueObject {
        #region Properties
        public string Body { get; }
        #endregion

        #region Constructor(s)
        public PostUpdate(string body) {
            Body = body;
        }
        #endregion
    }
}