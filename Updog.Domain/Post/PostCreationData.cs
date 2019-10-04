namespace Updog.Domain {
    public sealed class PostCreationData : IValueObject {
        #region Properties
        public PostType Type { get; }
        public string Title { get; }
        public string Body { get; }
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public PostCreationData(PostType type, string title, string body, string space) {
            Type = type;
            Title = title;
            Body = body;
            Space = space;
        }
        #endregion
    }
}