namespace Updog.Domain {
    public sealed class CommentCreateEvent : IDomainEvent {
        #region Properties
        public Comment Comment { get; }
        #endregion

        #region Constructor(s)
        public CommentCreateEvent(Comment comment) {
            Comment = comment;
        }
        #endregion
    }
}