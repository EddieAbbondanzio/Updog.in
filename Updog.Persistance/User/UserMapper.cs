using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    public interface IUserMapper : IReversableMapper<UserRecord, User> { }

    /// <summary>
    /// Mapper to convert the user record into it's user entity.
    /// </summary>
    public sealed class UserMapper : IUserMapper {
        #region Publics
        /// <summary>
        /// Convert the user record into an entity.
        /// </summary>
        /// <param name="source">The record to convert.</param>
        /// <returns>The rebuilt user entity.</returns>
        public User Map(UserRecord source) => new User(
            source.Id,
            source.Username,
            source.Email,
            source.PasswordHash,
            source.JoinedDate,
            source.PostKarma,
            source.CommentKarma
        );

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