using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    public interface ICommentMapper : IReversableMapper<CommentRecord, Comment> { }

    /// <summary>
    /// Mapper to convert a record to its entity.
    /// </summary>
    public sealed class CommentMapper : ICommentMapper {
        #region Publics
        /// <summary>
        /// Convert the record into it's entity.
        /// </summary>
        /// <param name="source">The initial row.</param>
        /// <returns>The resulting entity.</returns>
        public Comment Map(CommentRecord source) => new Comment() {
            Id = source.Id,
            UserId = source.UserId,
            PostId = source.PostId,
            ParentId = source.ParentId,
            Body = source.Body,
            CreationDate = source.CreationDate,
            WasUpdated = source.WasUpdated,
            WasDeleted = source.WasDeleted,
            Upvotes = source.Upvotes,
            Downvotes = source.Downvotes
        };

        /// <summary>
        /// Convert the entity back to it's row.
        /// </summary>
        /// <param name="destination">The post entity to convert.</param>
        /// <returns>The rebuilt record.</returns>
        public CommentRecord Reverse(Comment destination) => new CommentRecord() {
            Id = destination.Id,
            UserId = destination.UserId,
            PostId = destination.PostId,
            Body = destination.Body,
            CreationDate = destination.CreationDate,
            WasUpdated = destination.WasUpdated,
            WasDeleted = destination.WasDeleted,
            Upvotes = destination.Upvotes,
            Downvotes = destination.Downvotes
        };
        #endregion
    }
}