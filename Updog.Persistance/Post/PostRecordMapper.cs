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
        public Post Map(PostRecord source) {
            return new Post() {
                Id = source.Id,
                User = userMapper.Map(source.User),
                Type = source.Type,
                Title = source.Title,
                Body = source.Body,
                CreationDate = source.CreationDate,
                WasUpdated = source.WasUpdated,
                WasDeleted = source.WasDeleted
            };
        }
        /// <summary>
        /// Convert the post back into it's record form.
        /// </summary>
        /// <param name="destination">The entity/</param>
        /// <returns>The rebuilt record.</returns>
        public PostRecord Reverse(Post destination) {
            return new PostRecord {
                Id = destination.Id,
                User = userMapper.Reverse(destination.User),
                Type = destination.Type,
                Title = destination.Title,
                Body = destination.Body,
                CreationDate = destination.CreationDate,
                WasUpdated = destination.WasUpdated,
                WasDeleted = destination.WasDeleted
            };
        }
        #endregion
    }
}