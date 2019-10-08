namespace Updog.Domain {
    public sealed class PostDeleteEvent : IDomainEvent {
        #region Properties
        public Post Post { get; }
        #endregion

        #region Constructor(s)
        public PostDeleteEvent(Post post) {
            Post = post;
        }
        #endregion
    }
}