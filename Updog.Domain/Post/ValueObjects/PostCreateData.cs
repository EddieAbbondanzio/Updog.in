namespace Updog.Domain {
    public sealed class PostCreateData : IValueObject {
        #region Properties
        public PostType Type { get; }
        public string Title { get; }
        public string Body { get; }
        public int SpaceId { get; }
        #endregion

        #region Constructor(s)
        public PostCreateData(PostType type, string title, string body, int spaceId) {
            Type = type;
            Title = title;
            Body = body;
            SpaceId = spaceId;
        }
        #endregion
    }
}