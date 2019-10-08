namespace Updog.Domain {
    public sealed class PostCreateEvent : IDomainEvent {
        #region Properties
        public Post Post { get; }
        #endregion

        #region Constructor(s)
        public PostCreateEvent(Post post) {
            Post = post;
        }
        #endregion
    }
}