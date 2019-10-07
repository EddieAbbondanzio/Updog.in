using System;
using Updog.Domain;

namespace Updog.Persistance {
    public interface ICommentReadViewMapper : IMapper<CommentRecord, CommentReadView> { }

    public sealed class CommentReadViewMapper : ICommentReadViewMapper {
        #region Publics
        public CommentReadView Map(CommentRecord source) => new CommentReadView() {
            Id = source.Id,
            PostId = source.PostId,
            Body = source.Body,
            CreationDate = source.CreationDate,
            WasUpdated = source.WasUpdated,
            WasDeleted = source.WasDeleted,
            Upvotes = source.Upvotes,
            Downvotes = source.Downvotes
        };
        #endregion
    }
}