namespace Updog.Domain {
    public sealed class PostUpdateEvent : IDomainEvent {
        #region Properties
        public Post Post { get; }
        #endregion

        #region Constructor(s)
        public PostUpdateEvent(Post post) {
            Post = post;
        }
        #endregion
    }
}