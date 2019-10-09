namespace Updog.Domain {
    public sealed class CommentUpdate : IValueObject {
        #region Properties
        public string Body { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdate(string body) {
            Body = body;
        }
        #endregion
    }
}