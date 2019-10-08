namespace Updog.Domain {
    public sealed class CommentUpdateEvent : IDomainEvent {
        #region Properties
        public Comment Comment { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdateEvent(Comment comment) {
            Comment = comment;
        }
        #endregion
    }
}