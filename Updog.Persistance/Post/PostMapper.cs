using System;
using System.Dynamic;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    public interface IPostMapper : IReversableMapper<PostRecord, Post> { }

    /// <summary>
    /// Mapper to convert a post record into it's entity and back.
    /// </summary>
    public sealed class PostMapper : IPostMapper {
        #region Publics
        /// <summary>
        /// Convert the record into it's entity form.
        /// </summary>
        /// <param name="source">The source record.</param>
        /// <returns>The entity.</returns>
        public Post Map(PostRecord source) => new Post() {
            Id = source.Id,
            UserId = source.UserId,
            Type = source.Type,
            Title = source.Title,
            Body = source.Body,
            CreationDate = source.CreationDate,
            WasUpdated = source.WasUpdated,
            WasDeleted = source.WasDeleted,
            CommentCount = source.CommentCount,
            SpaceId = source.SpaceId,
            Upvotes = source.Upvotes,
            Downvotes = source.Downvotes
        };

        /// <summary>
        /// Convert the post back into it's record form.
        /// </summary>
        /// <param name="destination">The entity/</param>
        /// <returns>The rebuilt record.</returns>
        public PostRecord Reverse(Post destination) => new PostRecord {
            Id = destination.Id,
            UserId = destination.UserId,
            Type = destination.Type,
            Title = destination.Title,
            Body = destination.Body,
            CreationDate = destination.CreationDate,
            WasUpdated = destination.WasUpdated,
            WasDeleted = destination.WasDeleted,
            CommentCount = destination.CommentCount,
            Upvotes = destination.Upvotes,
            Downvotes = destination.Downvotes,
            SpaceId = destination.SpaceId
        };
        #endregion
    }
}