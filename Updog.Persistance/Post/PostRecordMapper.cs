using System;
using System.Dynamic;
using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert a post record into it's entity and back.
    /// </summary>
    public sealed class PostRecordMapper : IPostRecordMapper {
        #region Fields
        /// <summary>
        /// Mapper to convert user records to entities and back.
        /// </summary>
        private IUserRecordMapper userMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post record mapper.
        /// </summary>
        /// <param name="userMapper">The user record mapper.</param>
        public PostRecordMapper(IUserRecordMapper userMapper) {
            this.userMapper = userMapper;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Convert the record into it's entity form.
        /// </summary>
        /// <param name="source">The source record.</param>
        /// <returns>The entity.</returns>
        public Post Map(Tuple<PostRecord, UserRecord> source) {
            return new Post() {
                Id = source.Item1.Id,
                User = userMapper.Map(source.Item2),
                Type = source.Item1.Type,
                Title = source.Item1.Title,
                Body = source.Item1.Body,
                CreationDate = source.Item1.CreationDate,
                WasUpdated = source.Item1.WasUpdated,
                WasDeleted = source.Item1.WasDeleted,
                CommentCount = source.Item1.CommentCount
            };
        }
        /// <summary>
        /// Convert the post back into it's record form.
        /// </summary>
        /// <param name="destination">The entity/</param>
        /// <returns>The rebuilt record.</returns>
        public Tuple<PostRecord, UserRecord> Reverse(Post destination) {
            PostRecord p = new PostRecord {
                Id = destination.Id,
                UserId = destination.User.Id,
                Type = destination.Type,
                Title = destination.Title,
                Body = destination.Body,
                CreationDate = destination.CreationDate,
                WasUpdated = destination.WasUpdated,
                WasDeleted = destination.WasDeleted,
                CommentCount = destination.CommentCount
            };

            UserRecord u = userMapper.Reverse(destination.User);
            return Tuple.Create(p, u);
        }
        #endregion
    }
}