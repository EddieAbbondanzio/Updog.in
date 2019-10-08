namespace Updog.Domain {
    public sealed class CommentDeleteEvent : IDomainEvent {
        #region Properties
        public Comment Comment { get; }
        #endregion

        #region Constructor(s)
        public CommentDeleteEvent(Comment comment) {
            Comment = comment;
        }
        #endregion
    }
}