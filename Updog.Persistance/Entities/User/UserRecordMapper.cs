using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert the user record into it's user entity.
    /// </summary>
    public sealed class UserRecordMapper : IReversableMapper<UserRecord, User> {
        #region Publics
        /// <summary>
        /// Convert the user record into an entity.
        /// </summary>
        /// <param name="source">The record to convert.</param>
        /// <returns>The rebuilt user entity.</returns>
        public User Map(UserRecord source) => new User() {
            Id = source.Id,
            Username = source.Username,
            Email = source.Email,
            PasswordHash = source.PasswordHash,
            JoinedDate = source.JoinedDate,
            PostKarma = source.PostKarma,
            CommentKarma = source.CommentKarma
        };

        /// <summary>
        /// Reverse the user entity back into it's record.
        /// </summary>
        /// <param name="destination">The starting entity.</param>
        /// <returns>The rebuilt record.</returns>
        public UserRecord Reverse(User destination) => new UserRecord() {
            Id = destination.Id,
            Username = destination.Username,
            Email = destination.Email,
            PasswordHash = destination.PasswordHash,
            JoinedDate = destination.JoinedDate,
            PostKarma = destination.PostKarma,
            CommentKarma = destination.CommentKarma
        };
        #endregion
    }
}