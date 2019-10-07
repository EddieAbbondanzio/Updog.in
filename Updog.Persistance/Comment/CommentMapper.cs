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
        public Comment Map(CommentRecord source) => new Comment(
            source.Id,
            source.UserId,
            source.PostId,
            source.ParentId,
            source.Body,
            new VoteStats(source.Upvotes, source.Downvotes),
            source.CreationDate,
            source.WasUpdated,
            source.WasDeleted
        );

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
            Upvotes = destination.Votes.Upvotes,
            Downvotes = destination.Votes.Downvotes
        };
        #endregion
    }
}