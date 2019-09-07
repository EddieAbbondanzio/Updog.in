using Updog.Application;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Mapper to convert the user record into it's user entity.
    /// </summary>
    public sealed class UserRecordMapper : IUserRecordMapper {
        #region Publics
        /// <summary>
        /// Convert the user record into an entity.
        /// </summary>
        /// <param name="source">The record to convert.</param>
        /// <returns>The rebuilt user entity.</returns>
        public User Map(UserRecord source) {
            return new User() {
                Id = source.Id,
                Username = source.Username,
                Email = source.Email,
                PasswordHash = source.PasswordHash,
                JoinedDate = source.JoinedDate
            };
        }

        /// <summary>
        /// Reverse the user entity back into it's record.
        /// </summary>
        /// <param name="destination">The starting entity.</param>
        /// <returns>The rebuilt record.</returns>
        public UserRecord Reverse(User destination) {
            return new UserRecord() {
                Id = destination.Id,
                Username = destination.Username,
                Email = destination.Email,
                PasswordHash = destination.PasswordHash,
                JoinedDate = destination.JoinedDate
            };
        }
        #endregion
    }
}