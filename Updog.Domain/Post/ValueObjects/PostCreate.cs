namespace Updog.Domain {
    public sealed class PostCreate : IValueObject {
        #region Properties
        public PostType Type { get; }
        public string Title { get; }
        public string Body { get; }
        #endregion

        #region Constructor(s)
        public PostCreate(PostType type, string title, string body) {
            Type = type;
            Title = title;
            Body = body;
        }
        #endregion
    }
}