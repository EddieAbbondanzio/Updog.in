using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a record to its entity.
    /// </summary>
    public sealed class CommentRecordMapper : ICommentRecordMapper {
        #region Publics
        /// <summary>
        /// Convert the record into it's entity.
        /// </summary>
        /// <param name="source">The initial row.</param>
        /// <returns>The resulting entity.</returns>
        public Comment Map(CommentRecord source) {
            Comment comment = new Comment() {
                Id = source.Id,
                UserId = source.UserId,
                PostId = source.PostId,
                Body = source.Body,
                CreationDate = source.CreationDate,
                WasUpdated = source.WasUpdated,
                WasDeleted = source.WasDeleted,
                Upvotes = source.Upvotes,
                Downvotes = source.Downvotes
            };

            if (source.ParentId != 0) {
                comment.Parent = new Comment() { Id = source.ParentId };
            }

            return comment;
        }

        /// <summary>
        /// Convert the entity back to it's row.
        /// </summary>
        /// <param name="destination">The post entity to convert.</param>
        /// <returns>The rebuilt record.</returns>
        public CommentRecord Reverse(Comment destination) {
            CommentRecord commentRec = new CommentRecord() {
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

            //Comments dont always have a parent.
            if (destination.Parent != null) {
                commentRec.ParentId = destination.Parent.Id;
            }

            return commentRec;
        }
        #endregion
    }
}