using System;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a record to its entity.
    /// </summary>
    public sealed class CommentRecordMapper : ICommentRecordMapper {
        #region Fields
        /// <summary>
        /// Mapper to convert a user to it's entity and back.
        /// </summary>
        private IUserRecordMapper userMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment record mapper.
        /// </summary>
        /// <param name="userMapper">The user mapper needed.</param>
        public CommentRecordMapper(IUserRecordMapper userMapper) {
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Convert the record into it's entity.
        /// </summary>
        /// <param name="source">The initial row.</param>
        /// <returns>The resulting entity.</returns>
        public Comment Map(Tuple<CommentRecord, UserRecord> source) {
            Comment comment = new Comment() {
                Id = source.Item1.Id,
                User = this.userMapper.Map(source.Item2),
                PostId = source.Item1.PostId,
                Body = source.Item1.Body,
                CreationDate = source.Item1.CreationDate,
                WasUpdated = source.Item1.WasUpdated,
                WasDeleted = source.Item1.WasDeleted,
                Upvotes = source.Item1.Upvotes,
                Downvotes = source.Item1.Downvotes
            };

            if (source.Item1.ParentId != 0) {
                comment.Parent = new Comment() { Id = source.Item1.ParentId };
            }

            return comment;
        }

        /// <summary>
        /// Convert the entity back to it's row.
        /// </summary>
        /// <param name="destination">The post entity to convert.</param>
        /// <returns>The rebuilt record.</returns>
        public Tuple<CommentRecord, UserRecord> Reverse(Comment destination) {
            CommentRecord commentRec = new CommentRecord() {
                Id = destination.Id,
                UserId = destination.User.Id,
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

            UserRecord userRec = this.userMapper.Reverse(destination.User);

            return Tuple.Create(commentRec, userRec);
        }
        #endregion
    }
}